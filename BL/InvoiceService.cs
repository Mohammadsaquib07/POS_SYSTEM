using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Products_Crud.DTOs;
using Products_Crud.DTOs.ResponseDtos;
using Products_Crud.Interfaces;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    /// <summary>
    /// Invoice Service - Handles POS Invoice Generation (Like a Supermarket)
    /// 
    /// Process Flow:
    /// 1. Customer adds items to shopping cart
    /// 2. Validate all items exist and quantities are valid
    /// 3. Calculate subtotal (sum of all items)
    /// 4. Calculate tax (18% in this system)
    /// 5. Calculate total (subtotal + tax)
    /// 6. Generate unique invoice number
    /// 7. Save invoice header to database
    /// 8. Save each line item to database
    /// 9. Return invoice ID
    /// </summary>
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceCreate _invoiceCreate;
        private readonly IInvoiceRead _invoiceRead;
        private readonly ICustomerCreate _customerRepository;
        private readonly IProductRepository _productRepository;
        
        public InvoiceService(IInvoiceCreate invoiceCreate, IInvoiceRead invoiceRead, ICustomerCreate customerRepository, IProductRepository productRepository)
        {
            _invoiceCreate = invoiceCreate;
            _invoiceRead = invoiceRead;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
        public async System.Threading.Tasks.Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Items == null || request.Items.Count == 0)
                throw new InvalidOperationException("Invoice must have at least one item. Please add items to the cart.");

            if (request.IsNewCustomer)
            {
                if (request.Customer == null)
                    throw new InvalidOperationException("Customer information is required for new customer.");

                ValidateCustomerData(request.Customer);
                request.CustomerId = await CreateNewCustomerAsync(request.Customer);
            }
            else
            {
                if (request.CustomerId == null || request.CustomerId <= 0)
                    throw new InvalidOperationException("Valid CustomerId is required. Please select a customer.");
            }

            var (subtotal, invoiceItems) = await ProcessAndValidateItemsAsync(request.Items);
            decimal taxAmount = CalculateTax(subtotal);
            decimal totalAmount = subtotal + taxAmount;

            var invoice = new Invoices
            {
                CustomerId = request.CustomerId!.Value,
                InvoiceNumber = GenerateInvoiceNumber(),
                InvoiceDate = request.InvoiceDate == default ? DateTime.UtcNow : request.InvoiceDate,
                Subtotal = subtotal,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                Notes = request.Notes,
                CreatedBy = "ERP_System"
            };

            int invoiceId = await _invoiceCreate.SaveInvoiceWithItemsAsync(invoice, invoiceItems);
            var savedInvoice = await _invoiceRead.GetInvoiceAsync(invoiceId);
            return MapToInvoiceResponseDto(savedInvoice);
        }
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }

        // ============================================
        // HELPER METHODS - Data Validation
        // ============================================
        private void ValidateCustomerData(CreateCustomerDto customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new InvalidOperationException("Customer name is required.");

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new InvalidOperationException("Customer email is required.");

            if (!IsValidEmail(customer.Email))
                throw new InvalidOperationException("Customer email format is invalid.");
        }

        // ============================================
        // HELPER METHODS - Item Processing
        // ============================================
        /// <summary>
        /// Process and validate all items in the shopping cart
        /// For each item:
        /// - Validate quantity is positive
        /// - Fetch product from database
        /// - Create invoice line item with product details
        /// - Accumulate subtotal
        /// </summary>
        private async System.Threading.Tasks.Task<(decimal subtotal, List<InvoiceItem> items)> ProcessAndValidateItemsAsync(List<InvoiceItemRequestDto> cartItems)
        {
            decimal subtotal = 0;
            var invoiceItems = new List<InvoiceItem>();

            foreach (var cartItem in cartItems)
            {
                if (cartItem.Quantity <= 0)
                    throw new InvalidOperationException($"Item quantity must be greater than 0.");

                var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {cartItem.ProductId} not found. Please add a valid product.");

                decimal lineTotal = product.Price * cartItem.Quantity;
                subtotal += lineTotal;

                var invoiceItem = new InvoiceItem
                {
                    ProductId = cartItem.ProductId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = cartItem.Quantity,
                    Total = lineTotal
                };

                invoiceItems.Add(invoiceItem);
            }

            if (subtotal <= 0)
                throw new InvalidOperationException("Subtotal must be greater than 0. Please add valid items.");

            return (subtotal, invoiceItems);
        }

        // ============================================
        // HELPER METHODS - Calculations (Tax, Invoice#)
        // ============================================
        /// <summary>
        /// Calculate tax amount (18% in this system)
        /// </summary>
        private decimal CalculateTax(decimal subtotal)
        {
            const decimal TAX_RATE = 0.18m; // 18% tax
            return subtotal * TAX_RATE;
        }

        /// <summary>
        /// Create a new customer record in database
        /// </summary>
        private async System.Threading.Tasks.Task<int> CreateNewCustomerAsync(CreateCustomerDto customerData)
        {
            try
            {
                var newCustomer = new Customers
                {
                    Name = customerData.Name.Trim(),
                    Email = (customerData.Email ?? "").Trim().ToLower()
                };

                int customerId = await _customerRepository.AddCustomerAsync(newCustomer);

                if (customerId <= 0)
                    throw new InvalidOperationException("Failed to create customer. Invalid customer ID returned.");

                return customerId;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating customer: {ex.Message}", ex);
            }
        }

        // ============================================
        // HELPER METHODS - Validation Functions
        // ============================================
        
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // ============================================
        // READ OPERATIONS
        // ============================================
        public async System.Threading.Tasks.Task<InvoiceResponseDto> GetInvoiceByIdAsync(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new InvalidOperationException("Invalid invoice ID");

            var invoice = await _invoiceRead.GetInvoiceAsync(invoiceId);
            return MapToInvoiceResponseDto(invoice);
        }

        public async System.Threading.Tasks.Task<List<InvoiceResponseDto>> GetAllInvoicesAsync()
        {
            var invoices = await _invoiceRead.GetAllInvoicesAsync();
            return invoices.Select(i => MapToInvoiceResponseDto(i)).ToList();
        }

        // ============================================
        // MAPPING HELPER
        // ============================================
        private InvoiceResponseDto MapToInvoiceResponseDto(Invoices invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            var customerDto = new CustomerResponseDto
            {
                CustomerId = invoice.CustomerId,
                Name = invoice.Customer?.Name ?? "Unknown",
                Email = invoice.Customer?.Email,
                Phone = invoice.Customer?.Phone,
                BillingAddress = invoice.Customer?.BillingAddress
            };

            var itemDtos = invoice.Items?.Select(item => new InvoiceItemResponseDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                LineTotal = item.Total
            }).ToList() ?? new List<InvoiceItemResponseDto>();

            return new InvoiceResponseDto
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                SubTotal = invoice.Subtotal,
                TaxAmount = invoice.TaxAmount,
                TotalAmount = invoice.TotalAmount,
                Notes = invoice.Notes,
                CreatedBy = invoice.CreatedBy,
                CreatedAt = invoice.CreatedAt,
                Customer = customerDto,
                Items = itemDtos
            };
        }
    }
}
