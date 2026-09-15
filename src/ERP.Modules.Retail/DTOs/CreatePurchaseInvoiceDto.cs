using Erp.Dtos.PurchaseInvoice;

namespace Erp.Dtos.CreatePurchase
{
    public class CreatePurchaseInvoiceDto
    {
        public int SupplierId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal AmountPaid { get; set; } = 0;      
        public string PaymentMode { get; set; } = "Cash";    
        public List<PurchaseInvoiceItemInputDto> Items { get; set; } = new();

    }
}