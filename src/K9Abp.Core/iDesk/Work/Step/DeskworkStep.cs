using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace K9Abp.iDeskCore.Work
{
    public class DeskworkStep: AuditedEntity<long>
    {
        [Required]
        public virtual int WorkId { get; set; }

        [ForeignKey("WorkId")]
        public virtual Deskwork Deskwork { get; set; }

        [Required]
        public int AssignerId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssignerName { get; set; }

        public int? ReceiverId { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(500)]
        public string Result { get; set; }

        public DateTime? CompletedTime { get; set; }

        [Required]
        public EWorkCompletion Completion { get; set; }
    }
}