using Erp.Dtos.PurchaseInvoice;

namespace Erp.Dtos.CreatePurchase
{
    public class CreatePurchaseInvoiceDto
    {
        public int SupplierId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
        public List<PurchaseInvoiceItemInputDto> Items { get; set; } = new();
    }
}