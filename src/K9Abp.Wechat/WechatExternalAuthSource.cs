using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Dependency;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.MultiTenancy;

namespace K9Abp.Wechat
{
    public class WechatExternalAuthSource : DefaultExternalAuthenticationSource<Tenant, User>, ITransientDependency
    {
        public override string Name => "wechat";

        public override Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            //TODO: authenticate user and return true or false
            return Task.FromResult(true);
        }
    }
}
