using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using ProductDemoPlugin.Product.Dto;
using K9Abp.Core.EntityDemo;

namespace ProductDemoPlugin.Product
{
    [AbpAuthorize]
    public class ProductService: AsyncCrudAppService<ProductDemo, ProductSummaryDto, int, ProductGetAllDto, ProductCreateDto>, IProductService
    {
        public ProductService(IRepository<ProductDemo> repository)
            : base(repository)
        {
        }
    }
}


