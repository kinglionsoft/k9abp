using Abp.Application.Services;
using Abp.Application.Services.Dto;
using K9Abp.Application.Authorization.Permissions.Dto;

namespace K9Abp.Application.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}

