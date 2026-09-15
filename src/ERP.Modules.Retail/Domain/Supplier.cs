using Erp.interfaces.Tenant;
using Erp.Model.PuchaseInvoicEntities;

namespace Erp.Model.Entities
{
public class Supplier:ITenantEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ContactPerson { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? GstNumber { get; set; }
    public SupplierPaymentMode PaymentMode { get; set; } = SupplierPaymentMode.Cash;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    public int CompanyId { get; set; } 
    public ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
}
public enum SupplierPaymentMode
{
    Cash = 0,
    Credit = 1,
    BankTransfer = 2
}
}