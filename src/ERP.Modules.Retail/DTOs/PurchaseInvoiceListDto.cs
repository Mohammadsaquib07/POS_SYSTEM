namespace Erp.Dto.PurchaseInvoiceList
{
    public class PurchaseInvoiceListDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string SupplierName { get; set; }
        public int SupplierId { get; set; }  
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}