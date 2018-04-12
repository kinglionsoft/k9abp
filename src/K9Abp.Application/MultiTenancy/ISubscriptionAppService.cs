using System.Threading.Tasks;
using Abp.Application.Services;

namespace K9Abp.Application.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}

