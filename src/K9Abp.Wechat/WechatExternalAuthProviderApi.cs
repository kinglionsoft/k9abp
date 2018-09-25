using System.Threading.Tasks;
using K9Abp.Web.Core.Authentication.External;

namespace K9Abp.Wechat
{
    public class WechatExternalAuthProviderApi: ExternalAuthProviderApiBase
    {
        public override Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            var openId = string.Empty;

            var user = new ExternalAuthUserInfo
            {
                Provider = "wechat",
                ProviderKey = "openId1",
                Name = "yc",
                Surname = "yc",
                EmailAddress = "yc@aspnetzero.com"
            };
            return Task.FromResult(user);
        }
    }
}