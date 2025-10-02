namespace Products_Crud.Model
{
    //Customer specific
    public interface ICustomerCreate
    {
        int AddCustomer(Customers customer);
    }
    public interface ICustomerRead
    {
        Customers? GetCustomer(int Id);
        IEnumerable<Customers> GetAllCustomers();   
    }
    //Invoice Specific
    public interface IInvoiceCreate
    {
        int AddInvoice(Invoices invoices);
        void AddInvoiceItem(InvoiceItem invoiceItem);
    }
    public interface IInvoiceRead
    {
        Invoices GetInvoice(int Id);
        IEnumerable<Invoices> GetInvoicesByCustomer(int customerId);
    }
}