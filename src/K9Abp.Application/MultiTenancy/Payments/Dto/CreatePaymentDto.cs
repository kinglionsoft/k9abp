using K9Abp.Core.Editions;
using K9Abp.Core.MultiTenancy.Payments;

namespace K9Abp.Application.MultiTenancy.Payments.Dto
{
    public class CreatePaymentDto
    {
        public int EditionId { get; set; }

        public EEditionPaymentType EditionPaymentType { get; set; }

        public EPaymentPeriodType? PaymentPeriodType { get; set; }

        public ESubscriptionPaymentGatewayType SubscriptionPaymentGatewayType { get; set; }
    }
}

