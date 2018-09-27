using System;
using System.Threading.Tasks;
using K9Abp.Web.Core.Authentication.External;

namespace K9Abp.Wechat
{
    public class WechatExternalAuthProviderApi: ExternalAuthProviderApiBase
    {
        public override Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            throw new NotImplementedException();
        }
    }
}