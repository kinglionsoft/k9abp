using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace K9Abp.Core.EntityDemo
{
    public class ProductDemo: Entity, IPassivable
    {
        public virtual string Name  { get; set; }
        public virtual bool IsActive { get; set; }

        public ProductDemo()
        {
            Name = "null";
            IsActive = true;
        }
    }
}
