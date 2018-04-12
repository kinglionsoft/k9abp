using System.Threading.Tasks;
using K9Abp.Application.Sessions.Dto;

namespace K9Abp.Web.Core.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}

