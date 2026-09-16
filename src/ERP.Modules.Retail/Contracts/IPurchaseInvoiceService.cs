using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Contracts
{
    public interface IPurchaseInvoiceService
    {
        Task<List<PurchaseInvoice>> GetAllAsync();
        Task<List<PurchaseInvoiceResponseDto>> CreateAsync(CreatePurchaseInvoiceDto dto, string? attachmentPath);
        Task<PurchaseInvoiceResponseDto> DeleteAsync(int id);
          Task<PurchaseInvoice?> GetByIdAsync(int id);
    }
}
