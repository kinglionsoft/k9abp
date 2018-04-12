using System.Threading.Tasks;
using K9Abp.Application.Configuration.Dto;

namespace K9Abp.Application.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

