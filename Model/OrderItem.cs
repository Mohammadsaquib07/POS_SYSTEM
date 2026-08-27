using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
    public class OrderItem:ITenantEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; } = null!;
        public int CompanyId { get; set; } 
    }
}