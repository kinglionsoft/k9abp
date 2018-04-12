using System.ComponentModel.DataAnnotations;

namespace K9Abp.Application.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}
