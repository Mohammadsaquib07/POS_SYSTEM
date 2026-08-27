using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
    public class Customers :ITenantEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }  
        public required string Name { get; set; }   
        public string? Email { get; set; }  
        public string? Phone { get; set; }
        public string? BillingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Invoices>? Invoices { get; set; }
        public int CompanyId { get; set; } 
    }
}
