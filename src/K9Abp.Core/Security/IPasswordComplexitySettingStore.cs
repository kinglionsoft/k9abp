using System.Threading.Tasks;

namespace K9Abp.Core.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}

