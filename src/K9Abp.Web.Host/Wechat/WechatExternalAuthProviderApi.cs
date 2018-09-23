﻿using System.Threading.Tasks;
using K9Abp.Web.Core.Authentication.External;

namespace K9Abp.Web.Host.Wechat
{
    public class WechatExternalAuthProviderApi: ExternalAuthProviderApiBase
    {
        public override Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            var openId = string.Empty;

            var user = new ExternalAuthUserInfo
            {
                Provider = "wechat",
                ProviderKey = "openId",
                Name= "admin",
                Surname= "admin",
                EmailAddress= "admin@aspnetzero.com"
            };

            return Task.FromResult(user);
        }
    }
}