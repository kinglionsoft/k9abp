using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace K9Abp.iDesk.Domain.Tag
{
    public class DeskworkTag: Entity, ISoftDelete, IPassivable
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}