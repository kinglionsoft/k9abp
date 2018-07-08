using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public class Promotion: FullAuditedEntity, IMustHaveTenant, IPassivable, IExtendableObject
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        [Required]
        public virtual int TenantId { get; set; }
        [Required]
        public virtual bool IsActive { get; set; }

        public string ExtensionData { get; set; }

        public Promotion()
        {
            IsActive = true;
        }

        public Promotion(string name, Dictionary<string, string> columns)
        {
            IsActive = true;
            Name = name;
            this.SetData("columns", columns);
        }
    }
}