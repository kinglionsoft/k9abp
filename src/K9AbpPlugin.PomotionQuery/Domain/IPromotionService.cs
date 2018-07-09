using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public interface IPromotionService: ITransientDependency
    {
        Task<Dictionary<int, string>> GetPromotionsAsync();
        Task<PromotionTarget> GetTargetAsync(string phone, int promotionId);
        Task<Promotion> GetPromotionAsync(int id);
    }
}