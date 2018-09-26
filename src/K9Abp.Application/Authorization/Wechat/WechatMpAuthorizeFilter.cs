using System.Net;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using IdentityServer4.Extensions;
using K9Abp.Core.Configuration;
using K9Abp.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace K9Abp.Application.Authorization
{
    /// <summary>
    /// 验证是否微信公众号认证
    /// </summary>
    public class WechatMpAuthorizeFilter : IAsyncAuthorizationFilter, ITransientDependency
    {
        private readonly ISettingManager _settingManager;

        public WechatMpAuthorizeFilter(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 是否在微信浏览器
            if (!context.HttpContext.Request.IsWechat())
            {
                context.Result = new ContentResult { Content = "请使用微信访问" };
                return;
            }

            if (!context.HttpContext.User.IsAuthenticated())
            {
                var openId = context.HttpContext.Session.GetString("WechatOpenId");
                if (!string.IsNullOrEmpty(openId)) return;

                var appId = await _settingManager.GetSettingValueAsync(AppSettings.TenantManagement.WechatAppId);
                var returnUrl = WebUtility.UrlEncode(context.HttpContext.Request.GetAbsoluteUrl("Wechat/BaseCallback?returnUrl=" + context.HttpContext.Request.GetEncodedUrl()));
                var authUrl = $@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={returnUrl}&response_type=code&scope=snsapi_base&state=k9#wechat_redirect";
                context.Result = new RedirectResult(authUrl);
            }
        }
    }
}