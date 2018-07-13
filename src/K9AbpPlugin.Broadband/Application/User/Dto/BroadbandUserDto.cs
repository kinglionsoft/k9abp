using Abp.AutoMapper;
using Abp.Organizations;

namespace K9AbpPlugin.Broadband.User
{
    [AutoMapFrom(typeof(BroadbandUser))]
    public class BroadbandUserDto: BroadbandUser
    {
        public long CountyId { get; set; }
        public string CountyName { get; set; }
        public long DistinctId { get; set; }
        public string DistinctName { get; set; }
        public string OrganizationUnitName { get; set; }
    }
}