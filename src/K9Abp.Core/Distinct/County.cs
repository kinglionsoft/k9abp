using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.Organizations
{
    [Table("AbpCounty")]
    public class County: Entity, IMustHaveTenant
    {
        [Required]
        public virtual int TenantId { get; set; }

        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [Required]
        public virtual int Order { get; set; }
    }
}