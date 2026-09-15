using Products_Crud.Model;

namespace Products_Crud.Common.Contracts
{
    // Customer specific
    public interface ICustomerCreate
    {
        System.Threading.Tasks.Task<int> AddCustomerAsync(Customer customer);
    }

    public interface ICustomerRead
    {
        Customer? GetCustomer(int Id);
        IEnumerable<Customer> GetAllCustomers();
    }

    // Invoice specific
    public interface IInvoiceCreate
    {
        System.Threading.Tasks.Task<int> AddInvoiceAsync(Invoice invoices);
        System.Threading.Tasks.Task AddInvoiceItemAsync(InvoiceItem invoiceItem);
        System.Threading.Tasks.Task<int> SaveInvoiceWithItemsAsync(Invoice invoice, System.Collections.Generic.List<InvoiceItem> items);
        System.Threading.Tasks.Task<Invoice> GetInvoiceByIdAsync(int invoiceId);
        System.Threading.Tasks.Task<List<Invoice>> GetAllInvoicesAsync();
    }

    public interface IInvoiceRead
    {
        System.Threading.Tasks.Task<Invoice> GetInvoiceAsync(int Id);
        System.Threading.Tasks.Task<IEnumerable<Invoice>> GetInvoicesByCustomerAsync(int customerId);
        System.Threading.Tasks.Task<List<Invoice>> GetAllInvoicesAsync();
    }
}
