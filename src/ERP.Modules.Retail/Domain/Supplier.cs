using ERP.Modules.Shared.Contracts;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Domain
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
