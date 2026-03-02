using Products_Crud.Model;

namespace Products_Crud.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BillingAddress { get; set; }
    }
    public class InvoiceItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateInvoiceRequest
    {
        public bool IsNewCustomer { get; set; }

        public int? CustomerId { get; set; } // REQUIRED if IsNewCustomer = false

        public CreateCustomerDto? Customer { get; set; } // REQUIRED if IsNewCustomer = true

        public DateTime InvoiceDate { get; set; }

        public string? Notes { get; set; }

        public List<InvoiceItemRequestDto> Items { get; set; } = new();
    }
}
