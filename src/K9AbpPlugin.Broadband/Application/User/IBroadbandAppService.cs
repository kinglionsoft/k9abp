using System.Collections.Generic;
using System.Threading.Tasks;

namespace K9AbpPlugin.Broadband.User
{
    public interface IBroadbandAppService
    {
        /// <summary>
        /// 获取宽带用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BroadbandUserDto> GetAsync(int id);

        /// <summary>
        /// 获取片区下各渠道的预警情况
        /// </summary>
        /// <param name="distinctId"></param>
        /// <returns></returns>
        Task<List<DistinctWarnSummaryDto>> GetDistinctWarnSummaryAsync(int distinctId);

        /// <summary>
        /// 获取渠道下的预警用户
        /// </summary>
        /// <param name="organizationUnitId"></param>
        /// <returns></returns>
        Task<ChannelWarnDto> GetChannelWarnAsync(long organizationUnitId);

        /// <summary>
        /// 生成离网预警
        /// </summary>
        /// <param name="threshold">离网阈值</param>
        /// <returns></returns>
        Task CheckWarningAsync(int threshold);
    }
}