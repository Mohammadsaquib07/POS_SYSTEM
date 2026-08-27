using Products_Crud.Model;

namespace Erp.Model.Entities.variantsproducts
{
    public class ProductVariant
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Items Item { get; set; }

        // store the combination e.g. ["Red","M"] as JSON text
        public string ValuesJson { get; set; }

        public string? Sku { get; set; }
        public decimal PurchasePrice { get; set; }
        public int StockQty { get; set; }
        public string Status { get; set; } = "Active";
    }
}