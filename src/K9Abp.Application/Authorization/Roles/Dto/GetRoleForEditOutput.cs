using System.Collections.Generic;
using K9Abp.Application.Authorization.Permissions.Dto;

namespace K9Abp.Application.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
