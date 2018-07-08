using System.Threading.Tasks;

namespace K9AbpPlugin.PomotionQuery.Domain
{
    public interface IPromotionService
    {
        Task<PromotionTarget> GetTargetAsync(string phone, int promotionId);
    }
}