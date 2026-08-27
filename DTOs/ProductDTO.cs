using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_Crud.DTOs
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public List<VariantDTO>? Variants { get; set; }
    }

    public class VariantDTO
    {
        public List<string> Values { get; set; } = new();
        public string? Sku { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? StockQty { get; set; }
        public string? Status { get; set; }
    }
}