using Abp.AutoMapper;
using Abp.Organizations;

namespace K9AbpPlugin.Broadband.User
{
    [AutoMapFrom(typeof(BroadbandUser))]
    public class BroadbandUserDto: BroadbandUser
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int DistinctId { get; set; }
        public string DistinctName { get; set; }
        public string OrganizationUnitName { get; set; }

        public void SetOrganization(DistinctOrganizationUnit ou)
        {
            CountyId = ou.Distinct.CountyId;
            CountyName = ou.Distinct.County.Name;
            DistinctId = ou.DistinctId;
            DistinctName = ou.Distinct.Name;
            OrganizationUnitName = ou.OrganizationUnit.DisplayName;
        }
    }
}