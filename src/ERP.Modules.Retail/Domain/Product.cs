using System.ComponentModel.DataAnnotations.Schema;
using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
    [Table("Products")]
    public class Product : ITenantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal CostPrice { get; set; } = 0m;
        public string? Category { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
        public int CompanyId { get; set; }
    }
}
