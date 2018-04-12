using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace K9Abp.Core.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}

