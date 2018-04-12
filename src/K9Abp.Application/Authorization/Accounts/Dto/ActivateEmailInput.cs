using System.ComponentModel.DataAnnotations;

namespace K9Abp.Application.Authorization.Accounts.Dto
{
    public class ActivateEmailInput
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }
    }
}
