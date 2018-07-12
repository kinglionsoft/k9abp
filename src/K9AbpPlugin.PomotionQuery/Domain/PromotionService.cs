using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using K9Abp.Core;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public class PromotionService : K9AbpDomainServiceBase, IPromotionService
    {
        private readonly IRepository<PromotionTarget, long> _targetRepository;
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IRepository<QueryLog, long> _logRepository;

        public PromotionService(IRepository<QueryLog, long> logRepository, IRepository<Promotion> promotionRepository, IRepository<PromotionTarget, long> targetRepository)
        {
            _logRepository = logRepository;
            _promotionRepository = promotionRepository;
            _targetRepository = targetRepository;
        }

        #region 查询

        public async Task<Dictionary<int, string>> GetPromotionsAsync()
        {
           return await _promotionRepository.GetAllWithoutTracking()
                .OrderByDescending(x => x.Id)
                .ToDictionaryAsync(x => x.Id, x => x.Name);
        }

        public async Task<Promotion> GetPromotionAsync(int id)
        {
            return await _promotionRepository.FirstOrDefaultWithoutTrackingAsync(x => x.Id == id);
        }

        public async Task<PromotionTarget> GetTargetAsync(string phone, int promotionId)
        {
            var target = await _targetRepository
                .GetAllWithoutTracking()
                .FirstOrDefaultAsync(x => x.Phone == phone && x.PromotionId == promotionId);
            if (target != null)
            {
                await _logRepository.InsertAsync(new QueryLog
                {
                    PromotionName =(await _promotionRepository.GetAsync(promotionId)).Name,
                    Key = phone
                });
            }

            return target;
        }

        #endregion
    }
}