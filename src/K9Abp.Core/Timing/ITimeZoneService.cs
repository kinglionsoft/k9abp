using System.Threading.Tasks;
using Abp.Configuration;

namespace K9Abp.Core.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}

