using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.interfaces.Tenant;
using Erp.Model.Entities.variantsproducts;

namespace Products_Crud.Model
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