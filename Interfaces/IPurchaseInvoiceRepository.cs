using Erp.Model.PuchaseInvoicEntities;

namespace Erp.interfaces.Purchase
{
    public interface IPurchaseInvoiceRepository
    {
        Task<List<PurchaseInvoice>> GetAllAsync();
    }
}