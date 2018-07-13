using System.Linq;
using Abp.AutoMapper;

namespace Abp.Organizations
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitCacheItem
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public OrganizationUnitCacheItem Parent { get; set; }

        public EOrganizationUnitType Type
        {
            get
            {
                var count = Code.ToCharArray().Count(x => x == '.');
                return (EOrganizationUnitType) count;
            }
        }
    }
}