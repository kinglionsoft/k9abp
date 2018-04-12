using Abp.Dependency;

namespace K9Abp.Core.MultiTenancy.Payments
{
    public interface IPaymentGatewayManagerFactory
    {
        IDisposableDependencyObjectWrapper<IPaymentGatewayManager> Create(ESubscriptionPaymentGatewayType gateway);
    }
}
