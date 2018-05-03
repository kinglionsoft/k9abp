using System.Threading.Tasks;

namespace K9Abp.iDesk.Work
{
    public interface IWorkAppService
    {
        Task<bool> Follow(long workId, long? followerId);
    }
}