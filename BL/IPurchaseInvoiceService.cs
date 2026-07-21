using Erp.Dto.PurchaseInvoiceList;
using Erp.Dtos.CreatePurchase;
using Erp.Dtos.PurchaseInvoiceResponse;

namespace Erp.Dto.PurchaseService
{
    public interface IPurchaseInvoiceService
    {
        Task<List<PurchaseInvoiceListDto>> GetAllAsync();
        Task<List<PurchaseInvoiceResponseDto>> CreateAsync(CreatePurchaseInvoiceDto dto);
    }
}