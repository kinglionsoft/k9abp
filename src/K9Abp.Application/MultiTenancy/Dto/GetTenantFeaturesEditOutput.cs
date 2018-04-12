using System.Collections.Generic;
using Abp.Application.Services.Dto;
using K9Abp.Application.Editions.Dto;

namespace K9Abp.Application.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}
