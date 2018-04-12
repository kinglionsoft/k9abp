using Abp.Application.Services.Dto;

namespace ProductDemoPlugin.Product.Dto
{
    public class ProductGetAllDto: PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}


