using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Configuration;
using K9Abp.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace K9Abp.Wechat.Controllers
{
    public class WechatController : AbpController
    {
        private readonly ISettingManager _settingManager;
        public WechatController(ISettingManager settingManager)
        {
            _settingManager = settingManager;
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
            var appId = await _settingManager.GetSettingValueAsync(AppSettings.TenantManagement.WechatAppId);
            var appSecret = await _settingManager.GetSettingValueAsync(AppSettings.TenantManagement.WechatAppSecret);
            var result = OAuthApi.GetAccessToken(appId, appSecret, code);
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            HttpContext.Session.SetString("WechatOpenId", result.openid);
            if (!string.IsNullOrEmpty(returnUrl) && AbpUrlHelper.IsLocalUrl(Request, returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Content($"认证成功: {result.openid}，无权访问");

            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //HttpContext.Session.SetString("OAuthAccessTokenStartTime", DateTime.Now.ToString());
            //HttpContext.Session.SetString("OAuthAccessToken", result.ToJson());

            ////因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            //try
            //{
            //    //已关注，可以得到详细信息
            //    var userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);

            //    if (!string.IsNullOrEmpty(returnUrl))
            //    {
            //        return Redirect(returnUrl);
            //    }

            //    return View("UserInfoCallback", userInfo);
            //}
            //catch (ErrorJsonResultException ex)
            //{
            //    //未关注，只能授权，无法得到详细信息
            //    //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
            //    return Content("用户已授权，授权Token：" + result);
            //}
        }

    }
}