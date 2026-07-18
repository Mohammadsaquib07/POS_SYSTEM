using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    /// <summary>
    /// EF Core based repository for Invoice and InvoiceItem operations
    /// Provides clean LINQ-based data access
    /// </summary>
    public class InvoiceRepository : IInvoiceCreate, IInvoiceRead
    {
        private readonly UserDbContext _context;

        public InvoiceRepository(UserDbContext context)
        {
            _context = context;
        }

        // --------- CREATE INVOICE ----------
        /// <summary>
        /// Add a new invoice to the database
        /// </summary>
        public async System.Threading.Tasks.Task<int> AddInvoiceAsync(Invoices invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            if (invoice.CustomerId <= 0)
                throw new InvalidOperationException("Valid CustomerId is required for invoice.");

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice.InvoiceId;
        }

        // --------- CREATE INVOICE ITEM ----------
        /// <summary>
        /// Add a line item to an invoice (called for each product in the invoice)
        /// </summary>
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

        // --------- READ INVOICE ----------
        /// <summary>
        /// Get a single invoice by ID with related data
        /// </summary>
        public async System.Threading.Tasks.Task<Invoices> GetInvoiceAsync(int id)
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

        /// <summary>
        /// Get all invoices for a specific customer
        /// </summary>
        public async System.Threading.Tasks.Task<IEnumerable<Invoices>> GetInvoicesByCustomerAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer ID must be greater than 0", nameof(customerId));

            return await _context.Invoices
                .Where(i => i.CustomerId == customerId)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        // --------- ADDITIONAL READ METHODS ----------
        /// <summary>
        /// Get all invoices in the system
        /// </summary>
        public async System.Threading.Tasks.Task<List<Invoices>> GetAllInvoicesAsync()
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// Get invoice by ID (for service layer)
        /// </summary>
        public async System.Threading.Tasks.Task<Invoices> GetInvoiceByIdAsync(int invoiceId)
        {
            return await GetInvoiceAsync(invoiceId); // Delegates to existing GetInvoiceAsync method
        }

        /// <summary>
        /// Save invoice header and items together in a single transaction.
        /// This method will also validate and decrement product stock.
        /// </summary>
        public async System.Threading.Tasks.Task<int> SaveInvoiceWithItemsAsync(Invoices invoice, List<InvoiceItem> items)
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
