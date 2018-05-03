using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
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
        public virtual long AssignerId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssignerName { get; set; }

        public long? ReceiverId { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [StringLength(500)]
        public string Result { get; internal set; }

        [Required]
        public virtual bool Done { get; internal set; }

        public DateTime? CompletionTime { get; internal set; }

        [Required]
        public virtual EWorkCompletion Completion { get; internal set; }

        public DeskworkStep()
        {
            Completion = EWorkCompletion.未完成;
        }

        /// <summary>
        /// Create a new step
        /// </summary>
        public DeskworkStep(long assignerId,
            string assignerName, 
            long? receiverId, 
            string receiverName, 
            string department)
            : this()
        {
            AssignerId = assignerId;
            AssignerName = assignerName;
            ReceiverId = receiverId;
            ReceiverName = receiverName;
            Department = department;
        }

        public void Complete(string result)
        {
            if (Done) return;

            Result = result;
            Done = true;
            CompletionTime = Clock.Now;
        }
    }
}