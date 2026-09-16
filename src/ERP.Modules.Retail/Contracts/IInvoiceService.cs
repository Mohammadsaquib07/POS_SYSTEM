using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using ERP.Modules.Retail.Domain;


namespace ERP.Modules.Retail.Contracts
{
    public interface IInvoiceService
    {
        System.Threading.Tasks.Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequest request);
        System.Threading.Tasks.Task<InvoiceResponseDto> GetInvoiceByIdAsync(int invoiceId);
        System.Threading.Tasks.Task<List<InvoiceResponseDto>> GetAllInvoicesAsync();
    }
}
