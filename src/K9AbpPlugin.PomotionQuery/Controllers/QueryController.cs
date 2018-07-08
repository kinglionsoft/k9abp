using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using K9AbpPlugin.PomotionQuery.Domain;
using K9AbpPlugin.PomotionQuery.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.PomotionQuery.Controllers
{
    [Route("promotion/[controller]/[action]")]
    public class QueryController: AbpController
    {
        private readonly IBulkRepository<PromotionTarget, long> _targetRepository;
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IRepository<QueryLog, long> _logRepository;

        public QueryController(IRepository<QueryLog, long> logRepository, IRepository<Promotion> promotionRepository, IBulkRepository<PromotionTarget, long> targetRepository)
        {
            _logRepository = logRepository;
            _promotionRepository = promotionRepository;
            _targetRepository = targetRepository;
        }

        #region 查询
        
        [Route("promotion/query")]
        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionRepository.GetAllWithoutTracking()
                .OrderByDescending(x => x.Id)
                .ToDictionaryAsync(x => x.Id, x => x.Name);
            return View(promotions);
        }

        public async Task<IActionResult> Data(string phone, int promotionId)
        {
            var target = await _targetRepository
                .GetAllWithoutTracking()
                .Include(x => x.Promotion)
                .FirstOrDefaultAsync(x => x.Phone == phone && x.PromotionId == promotionId);
            if (target == null)
            {
                return View(new TargetQueryOutput
                {
                    PromotionId = promotionId,
                    Phone = phone,
                    Error = $"没有查询到可用数据"
                });
            }

            await _logRepository.InsertAsync(new QueryLog
            {
                PromotionName = target.Promotion.Name,
                Key = phone
            });
            
            var output = new TargetQueryOutput
            {
                PromotionId = promotionId,
                PromotionName = target.Promotion.Name,
                Columns = target.Promotion.GetData<Dictionary<string, string>>("columns"),
                Phone = phone,
                Row = target.GetData<Dictionary<string, string>>("data"),
            };

            return View(output);
        }

        #endregion


    }
}