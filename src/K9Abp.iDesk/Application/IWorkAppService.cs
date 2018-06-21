using System.Threading.Tasks;

namespace K9Abp.iDesk.Application
{
    public interface IWorkAppService
    {
        Task<bool> Follow(long workId, long? followerId);
    }
}