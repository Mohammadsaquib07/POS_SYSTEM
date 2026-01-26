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
        public int AddInvoice(Invoices invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            if (invoice.CustomerId <= 0)
                throw new InvalidOperationException("Valid CustomerId is required for invoice.");

            _context.Invoices.Add(invoice);
            _context.SaveChanges();
            return invoice.InvoiceId;
        }

        // --------- CREATE INVOICE ITEM ----------
        /// <summary>
        /// Add a line item to an invoice (called for each product in the invoice)
        /// </summary>
        public void AddInvoiceItem(InvoiceItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.InvoiceId <= 0)
                throw new InvalidOperationException("Valid InvoiceId is required for invoice item.");

            if (item.Quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than 0.");

            _context.InvoiceItems.Add(item);
            _context.SaveChanges();
        }

        // --------- READ INVOICE ----------
        /// <summary>
        /// Get a single invoice by ID with related data
        /// </summary>
        public Invoices GetInvoice(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invoice ID must be greater than 0", nameof(id));

            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
                throw new InvalidOperationException($"Invoice with ID {id} not found");

            return invoice;
        }

        /// <summary>
        /// Get all invoices for a specific customer
        /// </summary>
        public IEnumerable<Invoices> GetInvoicesByCustomer(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer ID must be greater than 0", nameof(customerId));

            return _context.Invoices
                .Where(i => i.CustomerId == customerId)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToList();
        }

        // --------- ADDITIONAL READ METHODS ----------
        /// <summary>
        /// Get all invoices in the system
        /// </summary>
        public List<Invoices> GetAllInvoices()
        {
            return _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToList();
        }

        /// <summary>
        /// Get invoice by ID (for service layer)
        /// </summary>
        public Invoices GetInvoiceById(int invoiceId)
        {
            return GetInvoice(invoiceId); // Delegates to existing GetInvoice method
        }
    }
}
