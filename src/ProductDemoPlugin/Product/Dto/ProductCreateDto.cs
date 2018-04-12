using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using K9Abp.Core.EntityDemo;

namespace ProductDemoPlugin.Product.Dto
{
    [AutoMapTo(typeof(ProductDemo))]
    public class ProductCreateDto: EntityDto
    {
        public string Name { get; set; }
    }
}


