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
