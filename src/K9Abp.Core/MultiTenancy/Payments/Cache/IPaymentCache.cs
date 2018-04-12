namespace K9Abp.Core.MultiTenancy.Payments.Cache
{
    public interface IPaymentCache
    {
        PaymentCacheItem GetCacheItemOrNull(ESubscriptionPaymentGatewayType gateway, string paymentId);

        void AddCacheItem(PaymentCacheItem item);

        void RemoveCacheItem(ESubscriptionPaymentGatewayType gateway, string paymentId);
    }
}

