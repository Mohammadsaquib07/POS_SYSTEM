namespace Products_Crud.Model
{
    public class InvoiceItem
    {
        public int ItemId { get; set; }
        public int InvoiceId { get; set; }           // FK
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        // 🔗 Navigation
        public Invoices? Invoice { get; set; }
    }
}
