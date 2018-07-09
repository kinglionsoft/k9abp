using System.ComponentModel.DataAnnotations;

namespace K9Abp.iDesk.Application.Dto
{
    public class FinishStepInput
    {
        [Required]
        public long WorkId { get; set; }
        [Required]
        public long StepId { get; set; }
        [Required]
        public string Result { get; set; }
        [Required]
        public long? ReceiverId { get; set; }
    }
}