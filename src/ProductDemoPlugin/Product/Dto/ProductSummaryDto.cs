using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using K9Abp.Core.EntityDemo;

namespace ProductDemoPlugin.Product.Dto
{
    [AutoMapFrom(typeof(ProductDemo))]
    public class ProductSummaryDto: EntityDto, IFullAudited
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}


