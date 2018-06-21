using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace K9Abp.Broadband
{
    public class BroadbrandUser: Entity
    {
        [StringLength(11)]
        [Column(TypeName = "char(11)")]
        public string Phone { get; set; }
    }
}