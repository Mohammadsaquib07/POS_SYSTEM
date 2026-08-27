namespace Erp.Dtos.Response.Variant
{
     public class VariantResponseDTO
    {
        public List<string> Values { get; set; } = new();
        public string? Sku { get; set; }
        public decimal PurchasePrice { get; set; }
        public int StockQty { get; set; }
        public string Status { get; set; }
    }
}