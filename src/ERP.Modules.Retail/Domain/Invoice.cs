using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
    public class Invoice : ITenantEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<InvoiceItem>? Items { get; set; }

        public int CompanyId { get; set; }
    }
}
