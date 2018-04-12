using System.Collections.Generic;
using K9Abp.Application.Authorization.Permissions.Dto;

namespace K9Abp.Application.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
