using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Abp.Configuration;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using K9Abp.Application.Authorization;
using K9Abp.Application.Security.Recaptcha;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Configuration;
using K9Abp.Core.Debugging;
using K9Abp.Core.Identity;
using K9Abp.Web.Core.Authentication.External;
using K9Abp.Web.Core.Authentication.JwtBearer;
using K9Abp.Web.Core.Controllers;
using K9Abp.Wechat.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace K9Abp.Wechat.Controllers
{
    public class WechatController : AuthorizeControllerBase
    {
        public IRecaptchaValidator RecaptchaValidator;
        private readonly IWechatService _wechatService;
        private readonly SignInManager _signInManager;

        public WechatController(LogInManager logInManager, 
            ITenantCache tenantCache, 
            AbpLoginResultTypeHelper abpLoginResultTypeHelper, 
            TokenAuthConfiguration configuration, 
            UserManager userManager, 
            ICacheManager cacheManager, 
            IOptions<JwtBearerOptions> jwtOptions, 
            IExternalAuthManager externalAuthManager, 
            IOptions<IdentityOptions> identityOptions, 
            IWechatService wechatService, 
            SignInManager signInManager) 
            : base(logInManager, tenantCache, abpLoginResultTypeHelper, configuration, userManager, cacheManager, jwtOptions, externalAuthManager, identityOptions)
        {
            _wechatService = wechatService;
            _signInManager = signInManager;
            RecaptchaValidator = NullRecaptchaValidator.Instance;
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="returnUrl">用户最初尝试进入的页面</param>
        /// <returns></returns>
        public async Task<IActionResult> BaseCallback(string code, string state, string returnUrl)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            //通过，用code换取access_token
            var appId = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.WechatAppId);
            var appSecret = await SettingManager.GetSettingValueAsync(AppSettings.TenantManagement.WechatAppSecret);
            var result = OAuthApi.GetAccessToken(appId, appSecret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            HttpContext.Session.SetString("WechatOpenId", result.openid);

           
            if (string.IsNullOrEmpty(returnUrl) || AbpUrlHelper.IsLocalUrl(Request, returnUrl))
            {
                return Content($"认证成功: {result.openid}，无权访问");
            }
            
            // 查找绑定的用户，使用外部认证源登录流程


            return Redirect(returnUrl);
            // 没有绑定用户，跳转到绑定页面

            return RedirectToAction("Bind", new {returnUrl});
        }

        /// <summary>
        /// 绑定微信
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(WechatMpAuthorizeFilter))]
        [HttpGet]
        public IActionResult Bind(string returnUrl)
        {
            ViewData["UseCaptcha"] = UseCaptchaOnRegistration();
            return View(new WechatBindInput
            {
                ReturnUrl = returnUrl
            });
        }

        [ServiceFilter(typeof(WechatMpAuthorizeFilter))]
        [HttpPost]
        public async Task<IActionResult> Bind(WechatBindInput input)
        {
            var openId = HttpContext.Session.GetString("WechatOpenId");
            if (string.IsNullOrEmpty(openId))
            {
                return Content("请从微信中访问");
            }

            if (UseCaptchaOnRegistration())
            {
                RecaptchaValidator.Validate(input.Captcha);
            }

            var tenancyName = GetTenancyNameOrNull();

           var loginResult = await LogInManager.LoginAsync(input.UserNameOrEmail, input.Password, tenancyName);
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                var exception =
                    AbpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result,
                        input.UserNameOrEmail, tenancyName);
                ViewData["Error"] = exception.Message;
                return View(input);
            }

            // 绑定
            await _wechatService.BindAsync(new WechtLoginInput
            {
                UserId = loginResult.User.Id,
                TenantId = loginResult.Tenant.Id,
                ProviderKey = openId
            });

            await _signInManager.SignInAsync(loginResult.User, true);
            if (AbpUrlHelper.IsLocalUrl(Request, input.ReturnUrl))
            {
                return Redirect(input.ReturnUrl);
            }
            return Redirect("/");
        }

        private bool UseCaptchaOnRegistration()
        {
            if (DebugHelper.IsDebug)
            {
                return false;
            }

            return SettingManager.GetSettingValue<bool>(AppSettings.UserManagement.UseCaptchaOnRegistration);
        }
    }
}