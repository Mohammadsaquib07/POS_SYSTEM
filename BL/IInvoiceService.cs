using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    public interface IInvoiceService
    {
        int CreateInvoice(InvoiceDto dto);
        //int CreateCustomerAndInvoice(CustomerDto custDto, InvoiceDto invoiceDto);
        int CreateCustomerAndInvoice(FullInvoiceRequest FullInvoiceRequestObj);
    }
}
