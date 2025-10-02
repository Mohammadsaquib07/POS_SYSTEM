using Products_Crud.DTOs;

namespace Products_Crud.Model
{
    public class FullInvoiceRequest
    {
        public bool IsNewCustomer { get; set; }
        public CustomerDto? Customer { get; set; }
        public InvoiceDto Invoice { get; set; }
    }

}
