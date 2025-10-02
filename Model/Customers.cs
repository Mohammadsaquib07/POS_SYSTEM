namespace Products_Crud.Model
{
    public class Customers
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
