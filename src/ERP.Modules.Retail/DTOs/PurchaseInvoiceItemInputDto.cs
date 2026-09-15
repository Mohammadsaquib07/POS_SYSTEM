namespace Erp.Dtos.PurchaseInvoice
{
    public class PurchaseInvoiceItemInputDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; } = 0;  
        public decimal TaxPercent { get; set; } = 0;         
        public string? BatchNumber { get; set; }           
        public DateTime? ExpiryDate { get; set; }
    }
}