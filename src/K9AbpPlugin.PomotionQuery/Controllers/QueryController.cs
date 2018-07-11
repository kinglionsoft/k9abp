using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Entities;
using K9AbpPlugin.PomotionQuery.Domain;
using K9AbpPlugin.PomotionQuery.Dto;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.PomotionQuery.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("promotion/[controller]/[action]")]
    public class QueryController: AbpController
    {
        private readonly IPromotionService _service;

        public QueryController(IPromotionService service)
        {
            _service = service;
        }

        #region 查询
        
        [HttpGet]
        [Route("/promotion/query")]
        public async Task<IActionResult> Index()
        {
            var promotions = await _service.GetPromotionsAsync();
            return View(promotions);
        }

        [HttpGet]
        public async Task<IActionResult> Data(int promotionId)
        {
            var promotion = await _service.GetPromotionAsync(promotionId);
            return View(promotion);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string phone, int promotionId)
        {
            TargetQueryOutput output;
            var promotion = await _service.GetPromotionAsync(promotionId);
            if (promotion == null)
            {
                output = new TargetQueryOutput
                {
                    PromotionId = promotionId,
                    Phone = phone,
                    PromotionName = "[无]",
                    Error = "查询项目不存在"
                };
            }
            else
            {
                var target = await _service.GetTargetAsync(phone, promotionId);

                if (target == null)
                {
                    output = new TargetQueryOutput
                    {
                        PromotionId = promotionId,
                        Phone = phone,
                        PromotionName = promotion.Name,
                        Error = "没有查询到可用数据"
                    };
                }
                else
                {
                    output = new TargetQueryOutput
                    {
                        PromotionId = promotionId,
                        PromotionName = promotion.Name,
                        Columns = promotion.GetData<Dictionary<string, string>>("columns"),
                        Phone = phone,
                        Row = target.GetData<Dictionary<string, string>>("data"),
                    };
                }
            }

            return View(output);
        }

        #endregion
    }
}