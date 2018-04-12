using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using K9Abp.Application.MultiTenancy.Accounting.Dto;

namespace K9Abp.Application.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}

