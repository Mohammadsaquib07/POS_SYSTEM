namespace Products_Crud.DTOs.ResponseDtos
{
    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public CustomerResponseDto Customer { get; set; } = null!;
        public List<InvoiceItemResponseDto> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }
    }

    public class CustomerResponseDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BillingAddress { get; set; }
    }

    public class InvoiceItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}