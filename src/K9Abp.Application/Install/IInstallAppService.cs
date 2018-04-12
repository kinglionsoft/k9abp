using System.Threading.Tasks;
using Abp.Application.Services;
using K9Abp.Application.Install.Dto;

namespace K9Abp.Application.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}
