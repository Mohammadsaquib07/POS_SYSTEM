using Erp.interfaces.Tenant;
using Erp.Model.Entities;
using Products_Crud.Common.Enums;
using Erp.Model.PurchaseInvoiceItemEntities;

namespace Erp.Model.PuchaseInvoicEntities
{
    public class PurchaseInvoice : ITenantEntity
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        public decimal SubTotal { get; set; }       
        public decimal TotalDiscount { get; set; }    
        public decimal TotalTax { get; set; }         
        public decimal TotalAmount { get; set; }      

        public decimal AmountPaid { get; set; }       
        public decimal BalanceDue { get; set; }        
        public string PaymentMode { get; set; }        

        public string? AttachmentPath { get; set; }   

        public PurchaseInvoiceStatus Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CompanyId { get; set; }
        public ICollection<PurchaseInvoiceItem> Items { get; set; } = new List<PurchaseInvoiceItem>();
    }
}
