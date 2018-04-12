using System.Threading.Tasks;

namespace K9Abp.Core.Identity
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}
