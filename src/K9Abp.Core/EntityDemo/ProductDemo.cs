using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace K9Abp.Core.EntityDemo
{
    [Audited]
    public class ProductDemo: FullAuditedEntity, IPassivable
    {
        public virtual string Name  { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
