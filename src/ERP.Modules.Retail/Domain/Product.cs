using System.ComponentModel.DataAnnotations.Schema;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using ERP.Modules.Shared.Contracts;

namespace ERP.Modules.Retail.Domain
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
