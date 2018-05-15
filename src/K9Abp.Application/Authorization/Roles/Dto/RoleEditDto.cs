using System.ComponentModel.DataAnnotations;

namespace K9Abp.Application.Authorization.Roles.Dto
{
    public class RoleEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public bool IsDefault { get; set; }
    }
}
