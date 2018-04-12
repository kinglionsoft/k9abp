using System.Threading.Tasks;
using Abp.Domain.Repositories;

namespace K9Abp.Core.MultiTenancy.Payments
{
    public interface ISubscriptionPaymentRepository : IRepository<SubscriptionPayment, long>
    {
        Task<SubscriptionPayment> UpdateByGatewayAndPaymentIdAsync(ESubscriptionPaymentGatewayType gateway, string paymentId, int? tenantId, SubscriptionPaymentStatus status);

        Task<SubscriptionPayment> GetByGatewayAndPaymentIdAsync(ESubscriptionPaymentGatewayType gateway, string paymentId);
    }
}

