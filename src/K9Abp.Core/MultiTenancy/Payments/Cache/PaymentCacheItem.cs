using System;

namespace K9Abp.Core.MultiTenancy.Payments.Cache
{
    [Serializable]
    public class PaymentCacheItem
    {
        public const string CacheName = "AppPaymentCache";

        public ESubscriptionPaymentGatewayType GateWay { get; set; }

        public string PaymentId { get; set; }

        public EPaymentPeriodType PaymentPeriodType { get; set; }

        private PaymentCacheItem()
        {
            
        }

        public PaymentCacheItem(ESubscriptionPaymentGatewayType gateWay, EPaymentPeriodType paymentPeriodType, string paymentId)
        {
            GateWay = gateWay;
            PaymentId = paymentId;
            PaymentPeriodType = paymentPeriodType;
        }
    }
}

