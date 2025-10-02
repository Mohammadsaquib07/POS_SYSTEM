using Products_Crud.Model;

namespace Products_Crud.DTOs
{
    public class InvoiceDto
    {
        public int CustomerId { get; set; }
        public bool IsnewCustomer { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public List<InvoiceItemDto> Items { get; set; } = new();
    }

    public class InvoiceItemDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CustomerDto
    {
        public int CustomerId { get; set; }   // PK
        public string Name { get; set; }      // NOT NULL
        public string? Email { get; set; }    // Nullable
        public string? Phone { get; set; }
        public string? BillingAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        // 🔗 Navigation Property (1 Customer -> Many Invoices)
        public ICollection<Invoices>? Invoices { get; set; }
    }

}
