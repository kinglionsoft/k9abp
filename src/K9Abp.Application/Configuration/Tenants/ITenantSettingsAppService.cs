using System.Threading.Tasks;
using Abp.Application.Services;
using K9Abp.Application.Configuration.Tenants.Dto;

namespace K9Abp.Application.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}

