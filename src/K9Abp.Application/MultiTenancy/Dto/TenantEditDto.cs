﻿using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using K9Abp.Core;

namespace K9Abp.Application.MultiTenancy.Dto
{
    public class TenantEditDto : EntityDto
    {
        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(K9AbpConsts.MaxNameLength)]
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }

        public DateTime? SubscriptionEndDateUtc { get; set; }

        public bool IsInTrialPeriod { get; set; }
    }
}
