using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public class PromotionTarget: Entity<long>, IMustHaveTenant, IExtendableObject
    {
        [Required]
        [StringLength(11)]
        [Column(TypeName= "char(11)")]
        public virtual string Phone { get; set; }

        [Required]
        public virtual int PromotionId { get; set; }

        [ForeignKey("PromotionId")]
        public virtual Promotion Promotion { get; set; }

        [Required]
        public virtual int TenantId { get; set; }

        public string ExtensionData { get; set; }

        public PromotionTarget()
        {
            
        }

        public PromotionTarget(string phone, int promotionId, Dictionary<string,string> columns)
        {
            Phone = phone;
            PromotionId = promotionId;
            this.SetData("data", columns);
        }
    }
}