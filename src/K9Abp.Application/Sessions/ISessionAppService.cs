using System.Threading.Tasks;
using Abp.Application.Services;
using K9Abp.Application.Sessions.Dto;

namespace K9Abp.Application.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}

