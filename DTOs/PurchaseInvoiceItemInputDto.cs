namespace Erp.Dtos.PurchaseInvoice
{
    public class PurchaseInvoiceItemInputDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}