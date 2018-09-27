using Abp.Authorization.Users;
using Abp.AutoMapper;

namespace K9Abp.Wechat.Services
{
    [AutoMapTo(typeof(UserLogin))]
    public class WechtLoginInput
    {
        public int TenantId { get; set; }

        public long UserId { get; set; }

        public string LoginProvider => "wechat";

        public string ProviderKey { get; set; }
    }
}