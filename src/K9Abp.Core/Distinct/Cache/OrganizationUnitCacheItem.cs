using Abp.AutoMapper;

namespace Abp.Organizations
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitCacheItem
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int DistinctId { get; set; }
        public string DistinctName { get; set; }
    }
}