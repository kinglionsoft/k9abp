using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using K9Abp.Application.Common.Dto;
using K9Abp.Application.Editions.Dto;

namespace K9Abp.Application.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}
