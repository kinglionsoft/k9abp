using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.Organizations
{
    [Table("AbpDistinctOrganizationUnit")]
    public class DistinctOrganizationUnit: Entity<long>
    {
        [Required]
        public virtual int DistinctId { get; set; }

        [ForeignKey("DistinctId")]
        public virtual Distinct Distinct { get; set; }

        [Required]
        public virtual long OrganizationUnitId { get; set; }

        [ForeignKey("OrganizationUnitId")]
        public virtual OrganizationUnit OrganizationUnit { get; set; }
    }
}