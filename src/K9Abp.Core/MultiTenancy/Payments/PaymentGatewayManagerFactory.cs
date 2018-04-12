using System;
using Abp.Dependency;

namespace K9Abp.Core.MultiTenancy.Payments
{
    public class PaymentGatewayManagerFactory : IPaymentGatewayManagerFactory, ITransientDependency
    {
        public IDisposableDependencyObjectWrapper<IPaymentGatewayManager> Create(ESubscriptionPaymentGatewayType gateway)
        {
            switch (gateway)
            {
                // case SubscriptionPaymentGatewayType.Paypal:
                //    return IocManager.Instance.ResolveAsDisposable<PayPalGatewayManager>();
                default:
                    throw new Exception("Can not create IPaymentGatewayManager for given gateway: " + gateway);
            }
        }
    }
}
