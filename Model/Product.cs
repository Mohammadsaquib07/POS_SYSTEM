using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
    public class Product:ITenantEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CompanyId { get; set; } 
    }

}