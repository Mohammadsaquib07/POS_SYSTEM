using ERP.Modules.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Domain
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
