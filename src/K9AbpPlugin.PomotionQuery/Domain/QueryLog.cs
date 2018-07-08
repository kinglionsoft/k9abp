using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    /// <summary>
    /// 查询日志
    /// </summary>
    public class QueryLog: CreationAuditedEntity<long>, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        public virtual string PromotionName { get; set; }
        public virtual string Key { get; set; }
        public virtual int TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }

        public QueryLog()
        {
            
        }

        public QueryLog(string promotionName, string key, int tenantId, long organizationUnitId)
        {
            PromotionName = promotionName;
            Key = key;
            TenantId = tenantId;
            OrganizationUnitId = organizationUnitId;
        }
    }
}