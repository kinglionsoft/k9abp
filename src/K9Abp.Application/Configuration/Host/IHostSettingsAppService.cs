using System.Threading.Tasks;
using Abp.Application.Services;
using K9Abp.Application.Configuration.Host.Dto;

namespace K9Abp.Application.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}

