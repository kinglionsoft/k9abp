using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.UI;
using K9Abp.Application;
namespace K9AbpPlugin.Broadband.User
{
    public class BroadbandAppService : K9AbpAppServiceBase, IBroadbandAppService
    {
        private readonly IBulkRepository<BroadbandUser> _userRepository;
        private readonly IDistinctOrganizationService _distinctOrganizationService;
        private readonly IObjectMapper _objectMapper;

        public BroadbandAppService(IBulkRepository<BroadbandUser> userUepository, IObjectMapper objectMapper, IDistinctOrganizationService distinctOrganizationService)
        {
            _userRepository = userUepository;
            _objectMapper = objectMapper;
            _distinctOrganizationService = distinctOrganizationService;
        }

        #region 导入



        #endregion

        #region 查询

        public async Task<BroadbandUserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new UserFriendlyException(404, $"用户({id})不存在");
            }
            var dto = _objectMapper.Map<BroadbandUserDto>(user);
            var ou = await _distinctOrganizationService.GetAsync(user.OrganizationUnitId);
            dto.SetOrganization(ou);
            return dto;
        }

        public async Task<List<WarnDto>> GetWarnUsersAsync(int distinctId)
        {
            var user = await GetAsync(1);
            return new List<WarnDto>
            {
                _objectMapper.Map<WarnDto>(user)
            };
        }

        #endregion
    }
}