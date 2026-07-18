namespace Products_Crud.Model
{
    //Customer specific
    public interface ICustomerCreate
    {
        System.Threading.Tasks.Task<int> AddCustomerAsync(Customers customer);
    }
    public interface ICustomerRead
    {
        Customers? GetCustomer(int Id);
        IEnumerable<Customers> GetAllCustomers();   
    }
    //Invoice Specific
    public interface IInvoiceCreate
    {
        System.Threading.Tasks.Task<int> AddInvoiceAsync(Invoices invoices);
        System.Threading.Tasks.Task AddInvoiceItemAsync(InvoiceItem invoiceItem);
        System.Threading.Tasks.Task<int> SaveInvoiceWithItemsAsync(Invoices invoice, System.Collections.Generic.List<InvoiceItem> items);
        System.Threading.Tasks.Task<Invoices> GetInvoiceByIdAsync(int invoiceId);
        System.Threading.Tasks.Task<List<Invoices>> GetAllInvoicesAsync();
    }
    public interface IInvoiceRead
    {
        System.Threading.Tasks.Task<Invoices> GetInvoiceAsync(int Id);
        System.Threading.Tasks.Task<IEnumerable<Invoices>> GetInvoicesByCustomerAsync(int customerId);
        System.Threading.Tasks.Task<List<Invoices>> GetAllInvoicesAsync();
    }
}