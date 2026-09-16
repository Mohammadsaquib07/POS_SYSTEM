using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.DTOs
{
    public class CreateCustomerDto
    {
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
    }
    public class InvoiceItemRequestDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateInvoiceRequest
    {
        public bool IsNewCustomer { get; set; }

        public int? CustomerId { get; set; } // REQUIRED if IsNewCustomer = false

        public CreateCustomerDto? Customer { get; set; } // REQUIRED if IsNewCustomer = true

        public DateTime InvoiceDate { get; set; }

        public string? Notes { get; set; }

        public List<InvoiceItemRequestDto> Items { get; set; } = new();
    }
}
