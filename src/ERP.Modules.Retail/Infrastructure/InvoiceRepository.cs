using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Infrastructure
{
    public class InvoiceRepository : IInvoiceCreate, IInvoiceRead
    {
        private readonly IRetailDbContext _context;

        public InvoiceRepository(IRetailDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task<int> AddInvoiceAsync(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            if (invoice.CustomerId <= 0)
                throw new InvalidOperationException("Valid CustomerId is required for invoice.");

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice.InvoiceId;
        }
        public async System.Threading.Tasks.Task AddInvoiceItemAsync(InvoiceItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.InvoiceId <= 0)
                throw new InvalidOperationException("Valid InvoiceId is required for invoice item.");

            if (item.Quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than 0.");

            await _context.InvoiceItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<Invoice> GetInvoiceAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invoice ID must be greater than 0", nameof(id));

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {id} not found");

            return invoice;
        }

        public async System.Threading.Tasks.Task<IEnumerable<Invoice>> GetInvoicesByCustomerAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer ID must be greater than 0", nameof(customerId));

            return await _context.Invoices
                .Where(i => i.CustomerId == customerId)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }
        public async System.Threading.Tasks.Task<List<Invoice>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }


        public async System.Threading.Tasks.Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            return await GetInvoiceAsync(invoiceId); 
        }


        public async System.Threading.Tasks.Task<int> SaveInvoiceWithItemsAsync(Invoice invoice, List<InvoiceItem> items)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));
            if (items == null) items = new List<InvoiceItem>();

            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Invoices.AddAsync(invoice);
                await _context.SaveChangesAsync();

                // Validate and update stock for each item
                foreach (var it in items)
                {
                    var product = await _context.Items.FindAsync(it.ProductId);
                    if (product == null)
                        throw new InvalidOperationException($"Product with ID {it.ProductId} not found.");

                    if (product.Stock < it.Quantity)
                        throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'.");

                    product.Stock -= it.Quantity;
                    _context.Items.Update(product);

                    it.InvoiceId = invoice.InvoiceId;
                    await _context.InvoiceItems.AddAsync(it);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return invoice.InvoiceId;
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
