using Erp.Dto.PurchaseInvoiceList;
using Erp.Dtos.CreatePurchase;
using Erp.Dtos.PurchaseInvoiceResponse;
using Erp.Model.PuchaseInvoicEntities;

namespace Erp.Dto.PurchaseService
{
    public interface IPurchaseInvoiceService
    {
        Task<List<PurchaseInvoice>> GetAllAsync();
        Task<List<PurchaseInvoiceResponseDto>> CreateAsync(CreatePurchaseInvoiceDto dto, string? attachmentPath);
        Task<PurchaseInvoiceResponseDto> DeleteAsync(int id);
          Task<PurchaseInvoice?> GetByIdAsync(int id);
    }
}