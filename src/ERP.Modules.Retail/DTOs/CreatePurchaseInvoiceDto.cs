using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.DTOs
{
    public class CreatePurchaseInvoiceDto
    {
        public int SupplierId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal AmountPaid { get; set; } = 0;      
        public string PaymentMode { get; set; } = "Cash";    
        public List<PurchaseInvoiceItemInputDto> Items { get; set; } = new();

    }
}
