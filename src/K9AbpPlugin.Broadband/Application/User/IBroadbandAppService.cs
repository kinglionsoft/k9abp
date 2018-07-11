using System.Collections.Generic;
using System.Threading.Tasks;

namespace K9AbpPlugin.Broadband.User
{
    public interface IBroadbandAppService
    {
        Task<BroadbandUserDto> GetAsync(int id);
        Task<List<WarnDto>> GetWarnUsersAsync(int distinctId);
    }
}