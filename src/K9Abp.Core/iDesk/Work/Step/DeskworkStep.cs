using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.UI;

namespace K9Abp.iDeskCore.Work
{
    public class DeskworkStep: AuditedEntity<long>
    {
        [Required]
        public virtual long WorkId { get; set; }

        [ForeignKey("WorkId")]
        public virtual Deskwork Deskwork { get; set; }

        [Required]
        public long AssignerId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssignerName { get; set; }

        public long? ReceiverId { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(500)]
        public string Result { get; internal set; }

        [Required]
        public bool Done { get; internal set; }

        public DateTime? CompletionTime { get; internal set; }

        [Required]
        public EWorkCompletion Completion { get; internal set; }

        public DeskworkStep()
        {
            Completion = EWorkCompletion.未完成;
        }

        public void Complete(string result)
        {
            if (Done) return;

            Result = result;
            Done = true;
            CompletionTime = DateTime.Now;
        }
    }
}