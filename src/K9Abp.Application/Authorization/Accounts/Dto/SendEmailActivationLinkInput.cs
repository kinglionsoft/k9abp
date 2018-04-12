using System.ComponentModel.DataAnnotations;

namespace K9Abp.Application.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
