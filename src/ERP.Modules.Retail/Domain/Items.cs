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
using ERP.Modules.Shared.Contracts;

namespace ERP.Modules.Retail.Domain
{
    public class Items:ITenantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public int Stock { get; set; }
         public List<ProductVariant> Variants { get; set; } = new();
         public int CompanyId { get; set; } 
    }
}
