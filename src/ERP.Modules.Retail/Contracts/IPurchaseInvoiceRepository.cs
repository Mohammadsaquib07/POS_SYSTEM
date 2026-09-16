using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Contracts
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
