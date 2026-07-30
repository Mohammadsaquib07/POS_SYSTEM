using Erp.Model.PuchaseInvoicEntities;
using Products_Crud.Model;

namespace Erp.Model.PurchaseInvoiceItemEntities
{
    public class PurchaseInvoiceItem
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }
        public int ProductId { get; set; }
        public ProductsList Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}