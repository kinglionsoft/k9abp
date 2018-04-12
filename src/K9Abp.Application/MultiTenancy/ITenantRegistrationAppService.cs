using System.Threading.Tasks;
using Abp.Application.Services;
using K9Abp.Application.Editions.Dto;
using K9Abp.Application.MultiTenancy.Dto;

namespace K9Abp.Application.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}
