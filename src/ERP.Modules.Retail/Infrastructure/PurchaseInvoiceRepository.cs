using Erp.interfaces.Purchase;
using Erp.Model.PuchaseInvoicEntities;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Contracts;

namespace Erp.Dal.PurchaseInvoiceImplementation
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly IRetailDbContext _context;
        public PurchaseInvoiceRepository(IRetailDbContext context) => _context = context;
 
        public async Task<List<PurchaseInvoice>> GetAllAsync()
        {
            return await _context.PurchaseInvoices
                .Include(pi => pi.Supplier)
                .OrderByDescending(pi => pi.InvoiceDate)
                .ToListAsync();
        }
 
        public async Task<PurchaseInvoice?> GetByIdAsync(int Id)
        {
            return await _context.PurchaseInvoices
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
 
        // NEW — used only by the delete-with-stock-reversal flow in the service.
        public async Task<PurchaseInvoice?> GetByIdWithItemsAndProductsAsync(int Id)
        {
            return await _context.PurchaseInvoices
                .Include(p => p.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }
 
        public async Task<bool> DeleteAsync(int Id)
        {
            var invoice = await _context.PurchaseInvoices.FindAsync(Id);
            if (invoice == null)
            {
                return false;
            }
            _context.PurchaseInvoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}