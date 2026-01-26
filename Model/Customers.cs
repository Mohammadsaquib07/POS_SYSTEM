using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products_Crud.Model
{
    public class Customers
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
    }
}
