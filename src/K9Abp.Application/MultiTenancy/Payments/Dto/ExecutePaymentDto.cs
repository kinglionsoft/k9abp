using System.Collections.Generic;
using K9Abp.Core.Editions;
using K9Abp.Core.MultiTenancy.Payments;

namespace K9Abp.Application.MultiTenancy.Payments.Dto
{
    public class ExecutePaymentDto
    {
        public ESubscriptionPaymentGatewayType Gateway { get; set; }

        public EEditionPaymentType EditionPaymentType { get; set; }

        public int EditionId { get; set; }

        public EPaymentPeriodType PaymentPeriodType { get; set; }

        public Dictionary<string, string> AdditionalData { get; set; }

        public ExecutePaymentDto()
        {
            AdditionalData = new Dictionary<string, string>();
        }
    }
}

