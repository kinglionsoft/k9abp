
using Abp.AutoMapper;

namespace K9Abp.Core.Authorization.Users
{
    [AutoMapFrom(typeof(User))]
    public class UserCacheItem
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public long OrganizationUnitId { get; set; }
        public string OrganizationUnitName { get; set; }

        public UserCacheItem()
        {
            OrganizationUnitName = K9AbpConsts.NA;
        }
    }
}