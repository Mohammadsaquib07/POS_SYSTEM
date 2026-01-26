using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    public interface IInvoiceService
    {
        int CreateInvoice(CreateInvoiceRequest request);
        int CreateCustomerAndInvoice(CreateInvoiceRequest FullInvoiceRequestObj);
        Invoices GetInvoiceById(int invoiceId);
        List<Invoices> GetAllInvoices();
    }
}
