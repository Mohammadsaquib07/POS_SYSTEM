using Erp.interfaces.Purchase;
using Erp.Model.PuchaseInvoicEntities;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;

namespace Erp.Dal.PurchaseInvoiceImplementation
{
    public class PurchaseInvoiceRepository:IPurchaseInvoiceRepository
    {
        private readonly UserDbContext _context;
        public PurchaseInvoiceRepository(UserDbContext context)=>_context = context;

        public async Task<List<PurchaseInvoice>> GetAllAsync()
        {
            return await _context.PurchaseInvoices
            .Include(pi => pi.Supplier)
            .OrderByDescending(pi => pi.InvoiceDate)
            .ToListAsync();
        }
    }
}