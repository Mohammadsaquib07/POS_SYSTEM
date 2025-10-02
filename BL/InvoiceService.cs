using Microsoft.EntityFrameworkCore.Diagnostics;
using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceCreate _invoiceCreate;
        private readonly ICustomerCreate _ICustomerCreate;
        public InvoiceService(IInvoiceCreate invoiceCreate, ICustomerCreate iCustomerCreate)
        {
            _invoiceCreate = invoiceCreate;
            _ICustomerCreate = iCustomerCreate;
        }
        public int CreateInvoice(InvoiceDto invoiceDto)
        {
            if (invoiceDto == null) throw new ArgumentNullException(nameof(invoiceDto));
            if (invoiceDto.Items == null || invoiceDto.Items.Count == 0)
                throw new InvalidOperationException("Invoice must have atleast one item");
            //------------Calculate Total------------
            decimal subtotal = invoiceDto.Items.Sum(x=> x.Price * x.Quantity);
            decimal taxamount = subtotal * 0.18m;  // 18% tax
            decimal totalamount = subtotal + taxamount;
            //------Prepare invoice entity-------
            var invoices = new Invoices
            {
                CustomerId = invoiceDto.CustomerId,
                InvoiceDate = invoiceDto.InvoiceDate,
                Subtotal = subtotal,
                TaxAmount = taxamount,
                TotalAmount = totalamount,
                Notes = invoiceDto.Notes,
                CreatedBy = invoiceDto.CreatedBy,
                InvoiceNumber = GenerateInvoiceNumber() // simple unique invoice number
            };
            // --------- 3️⃣ Insert master invoice ----------
            int invoiceId = _invoiceCreate.AddInvoice(invoices);
            // --------- 4️⃣ Insert all invoice items ----------
            foreach(var item in invoiceDto.Items)
            {
                var invoiceItems = new InvoiceItem
                {
                    InvoiceId = invoiceId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Total = item.Price * item.Quantity,
                };
                _invoiceCreate.AddInvoiceItem(invoiceItems);
            }
            // --------- 5️⃣ Return new InvoiceId ----------
            return invoiceId;
        }
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.Now:yyyyMMddHHmmss}";
        }
        public int CreateCustomerAndInvoice(FullInvoiceRequest obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (obj.Customer == null) throw new InvalidOperationException("Customer information is required for a new customer.");
            var newCustomer = new Customers
            {
                Name = obj.Customer.Name,
                Email = obj.Customer.Email,
                Phone = obj.Customer.Phone,
                BillingAddress = obj.Customer.BillingAddress
            };
            int customerId = _ICustomerCreate.AddCustomer(newCustomer);
            obj.Invoice.CustomerId = customerId;
            return CreateInvoice(obj.Invoice);
        }
    }
}
