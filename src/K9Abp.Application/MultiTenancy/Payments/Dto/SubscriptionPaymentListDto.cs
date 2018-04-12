﻿using Abp.Application.Services.Dto;

namespace K9Abp.Application.MultiTenancy.Payments.Dto
{
    public class SubscriptionPaymentListDto: AuditedEntityDto
    {
        public string Gateway { get; set; }

        public decimal Amount { get; set; }

        public int EditionId { get; set; }

        public int DayCount { get; set; }

        public string PaymentPeriodType { get; set; }

        public string PaymentId { get; set; }

        public string PayerId { get; set; }

        public string Status { get; set; }

        public string EditionDisplayName { get; set; }

        public int TenantId { get; set; }

        public string InvoiceNo { get; set; }

    }
}
