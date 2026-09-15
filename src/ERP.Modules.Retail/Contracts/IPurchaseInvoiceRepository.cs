using Erp.Model.PuchaseInvoicEntities;

namespace Erp.interfaces.Purchase
{
    // public interface IPurchaseInvoiceRepository
    // {
    //     Task<List<PurchaseInvoice>> GetAllAsync();
    //     Task<PurchaseInvoice?> GetByIdAsync(int Id);
    //     Task<bool> DeleteAsync(int Id);
    // }
    public interface IPurchaseInvoiceRepository
    {
        Task<List<PurchaseInvoice>> GetAllAsync();
        Task<PurchaseInvoice?> GetByIdAsync(int Id);
        Task<PurchaseInvoice?> GetByIdWithItemsAndProductsAsync(int Id);
        Task<bool> DeleteAsync(int Id);
    }
}