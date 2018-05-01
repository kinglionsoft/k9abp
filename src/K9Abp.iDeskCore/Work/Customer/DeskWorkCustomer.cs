using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Auditing;

namespace K9Abp.iDeskCore.Work.Customer
{
    public class DeskWorkCustomer: AuditedAttribute
    {
        [Required]
        [StringLength(11)]
        [Column(TypeName= "char(11)")]
        public string Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }
    }
}