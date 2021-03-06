﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.UI;
using K9Abp.Application;
using K9AbpPlugin.Broadband.Warn;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.Broadband.User
{
    [RemoteService(isEnabled: false)]
    public class BroadbandAppService : K9AbpAppServiceBase, IBroadbandAppService
    {
        private readonly IRepository<BroadbandUser> _userRepository;
        private readonly IRepository<BroadbandWarn> _warnRepository;
        private readonly IOrganizationUnitCache _organizationUnitCache;
        private readonly OrganizationUnitManager _organizationUnitManager;

        public BroadbandAppService(IRepository<BroadbandUser> userUepository,
            OrganizationUnitManager organizationUnitManager, 
            IRepository<BroadbandWarn> warnRepository,
            IOrganizationUnitCache organizationUnitCache)
        {
            _userRepository = userUepository;
            _organizationUnitManager = organizationUnitManager;
            _warnRepository = warnRepository;
            _organizationUnitCache = organizationUnitCache;
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
            var dto = ObjectMapper.Map<BroadbandUserDto>(user);
            var ou = await _organizationUnitCache.GetAsync(dto.OrganizationUnitId);
            if (ou.Type == EOrganizationUnitType.Channel)
            {
                dto.OrganizationUnitName = ou.DisplayName;
                dto.DistinctId = ou.Parent.Id;
                dto.DistinctName = ou.Parent.DisplayName;
                var county = await _organizationUnitCache.GetAsync(dto.DistinctId);
                dto.CountyId = county.Id;
                dto.CountyName = county.DisplayName;
            }
            return dto;
        }

        /// <summary>
        /// 获取片区下各渠道的预警情况
        /// </summary>
        /// <param name="distinctId"></param>
        /// <returns></returns>
        public async Task<List<DistinctWarnSummaryDto>> GetDistinctWarnSummaryAsync(int distinctId)
        {
            var result = await _warnRepository.GetAllWithoutTracking()
                    .Where(x => x.DistinctId == distinctId)
                    .GroupBy(x => x.OrganizationUnitId)
                    .Select(x => new DistinctWarnSummaryDto
                    {
                        OrganizationUnitId = x.Key,
                        Count = x.Count()
                    }).ToListAsync();
            foreach (var dto in result)
            {
                dto.OrganizationUnitName = _organizationUnitCache.Get(dto.OrganizationUnitId).DisplayName;
            }
            return result;
        }

        /// <summary>
        /// 获取渠道下的预警用户
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        public async Task<ChannelWarnDto> GetChannelWarnAsync(long organizationUnitId)
        {
            var users = from u in _userRepository.GetAllWithoutTracking()
                join w in _warnRepository.GetAllWithoutTracking() on u.Id equals w.UserId
                where w.OrganizationUnitId == organizationUnitId
                select u;
            var result = new ChannelWarnDto();
            result.Users = ObjectMapper.Map<List<WarnUserDto>>(await users.ToListAsync());
            var ou = _organizationUnitCache.Get(organizationUnitId);
            result.OrganizationUnitId = organizationUnitId;
            result.OrganizationUnitName = ou.DisplayName;
            if (ou.Type == EOrganizationUnitType.Channel)
            {
                result.OrganizationUnitName = ou.DisplayName;
                result.DistinctId = ou.Parent.Id;
                result.DistinctName = ou.Parent.DisplayName;
                var county = await _organizationUnitCache.GetAsync(result.DistinctId);
                result.CountyId = county.Id;
                result.CountyName = county.DisplayName;
            }
            return result;
        }

        #endregion

        #region 预警统计

        /// <summary>
        /// 生成离网预警
        /// </summary>
        /// <param name="threshold">离网阈值</param>
        /// <returns></returns>
        public Task CheckWarningAsync(int threshold)
        {
            return _userRepository.ExecuteSqlAsync($@"
delete from plugin_broadband_warn;
insert into plugin_broadband_warn(OrganizationUnitId,TenantId,UserId,CountyId,DistinctId)
select a.OrganizationUnitId,
a.TenantId,
a.Id,
c.ParentId,
b.ParentId
from plugin_broadband_user a inner join abp_organization_unit b on a.OrganizationUnitId = b.Id
inner join abp_organization_unit c on b.ParentId = c.Id
where DATE_ADD(now(), INTERVAL {threshold} Day) > a.ExpireTime;
", null);
        }

        #endregion
    }
}