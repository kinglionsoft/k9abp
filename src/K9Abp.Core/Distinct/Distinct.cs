using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.Organizations
{
    /// <summary>
    /// 组织机构片区
    /// </summary>
    [Table("AbpDistinct")]
    public class Distinct: Entity, IMustHaveTenant
    {
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }

        [Required]
        public virtual int CountyId { get; set; }

        [ForeignKey("CountyId")]
        public virtual County County { get; set; }

        [Required]
        public virtual int Order { get; set; }

        [Required]
        public virtual int TenantId { get; set; }
    }
}