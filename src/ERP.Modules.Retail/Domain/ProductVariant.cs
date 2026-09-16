using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Domain
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
