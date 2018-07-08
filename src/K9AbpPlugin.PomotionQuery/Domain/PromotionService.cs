using System.Threading.Tasks;
using Abp.Domain.Repositories;
using K9Abp.Core;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public class PromotionService : K9AbpDomainServiceBase, IPromotionService
    {
        private readonly IBulkRepository<PromotionTarget, long> _targetRepository;
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IRepository<QueryLog, long> _logRepository;

        public PromotionService(IRepository<QueryLog, long> logRepository, IRepository<Promotion> promotionRepository, IBulkRepository<PromotionTarget, long> targetRepository)
        {
            _logRepository = logRepository;
            _promotionRepository = promotionRepository;
            _targetRepository = targetRepository;
        }

        #region 查询

        public async Task<PromotionTarget> GetTargetAsync(string phone, int promotionId)
        {
            var target = await _targetRepository
                .GetAllWithoutTracking()
                .Include(x=> x.Promotion)
                .FirstOrDefaultAsync(x => x.Phone == phone && x.PromotionId == promotionId);
            if (target != null)
            {
                await _logRepository.InsertAsync(new QueryLog
                {
                    PromotionName = target.Promotion.Name,
                    Key = phone
                });
            }

            return target;
        }

        #endregion
    }
}