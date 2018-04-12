using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using K9Abp.Application.Configuration.Dto;
using K9Abp.Core.Configuration;

namespace K9Abp.Application.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : K9AbpAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

