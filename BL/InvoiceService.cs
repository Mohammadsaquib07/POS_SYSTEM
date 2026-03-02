using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Products_Crud.DTOs;
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
        public  int CreateInvoice(CreateInvoiceRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            // Step 1️⃣ : Validate customer ID
            if (request.CustomerId == null || request.CustomerId <= 0)
                throw new InvalidOperationException("Valid CustomerId is required. Please select a customer.");

            // Step 2️⃣ : Validate items exist
            if (request.Items == null || request.Items.Count == 0)
                throw new InvalidOperationException("Invoice must have at least one item. Please add items to the cart.");

            // Step 3️⃣ : Process items and calculate totals
            var (subtotal, invoiceItems) = ProcessAndValidateItems(request.Items);

            // Step 4️⃣ : Calculate tax and total
            decimal taxAmount = CalculateTax(subtotal);
            decimal totalAmount = subtotal + taxAmount;

            // Step 5️⃣ : Create invoice header
            var invoice = new Invoices
            {
                CustomerId = request.CustomerId.Value,
                InvoiceNumber = GenerateInvoiceNumber(),
                InvoiceDate = request.InvoiceDate == default ? DateTime.UtcNow : request.InvoiceDate,
                Subtotal = subtotal,
                TaxAmount = taxAmount,
                TotalAmount = totalAmount,
                Notes = request.Notes,
                CreatedBy = "POS_System"
            };

            // Step 6️⃣ : Save invoice header
            int invoiceId = _invoiceCreate.AddInvoice(invoice);
            // var invoiceDetailes = _invoiceCreate.AddInvoice(invoice);

            // Step 7️⃣ : Save all line items
            foreach (var item in invoiceItems)
            {
                item.InvoiceId = invoiceId;
                _invoiceCreate.AddInvoiceItem(item);
            }

            return invoiceId;
        }
        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMddHHmmss}";
        }
        public int CreateCustomerAndInvoice(CreateInvoiceRequest request)
        {
            if (request == null) 
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null) 
                throw new InvalidOperationException("Customer information is required for new customer.");

            // --------- STEP 1: Validate Customer Fields ----------
            ValidateCustomerData(request.Customer);

            // --------- STEP 2: Create Customer ----------
            int customerId = CreateNewCustomer(request.Customer);

            // --------- STEP 3: Create Invoice for New Customer ----------
            request.CustomerId = customerId;
            return CreateInvoice(request);
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

            if (string.IsNullOrWhiteSpace(customer.Phone))
                throw new InvalidOperationException("Customer phone is required.");

            if (!IsValidPhone(customer.Phone))
                throw new InvalidOperationException("Customer phone format is invalid. Phone must be 10-15 digits.");

            if (string.IsNullOrWhiteSpace(customer.BillingAddress))
                throw new InvalidOperationException("Customer billing address is required.");
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
        private (decimal subtotal, List<InvoiceItem> items) ProcessAndValidateItems(List<InvoiceItemRequestDto> cartItems)
        {
            decimal subtotal = 0;
            var invoiceItems = new List<InvoiceItem>();

            foreach (var cartItem in cartItems)
            {
                // Validate quantity
                if (cartItem.Quantity <= 0)
                    throw new InvalidOperationException($"Item quantity must be greater than 0.");

                // Fetch product from database
                var product = _productRepository.GetByIdAsync(cartItem.ProductId).Result;
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {cartItem.ProductId} not found. Please add a valid product.");

                // Calculate line total
                decimal lineTotal = product.Price * cartItem.Quantity;
                subtotal += lineTotal;

                // Create invoice line item
                var invoiceItem = new InvoiceItem
                {
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
        private int CreateNewCustomer(CreateCustomerDto customerData)
        {
            try
            {
                var newCustomer = new Customers
                {
                    Name = customerData.Name.Trim(),
                    Email = customerData.Email.Trim().ToLower(),
                    Phone = customerData.Phone.Trim(),
                    BillingAddress = customerData.BillingAddress.Trim()
                };

                int customerId = _customerRepository.AddCustomer(newCustomer);
                
                if (customerId <= 0)
                    throw new InvalidOperationException("Failed to create customer. Invalid customer ID returned.");

                return customerId;
            }
            catch (InvalidOperationException)
            {
                throw; // Re-throw validation errors as-is
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
        
        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            
            // Remove common phone separators
            string cleanPhone = System.Text.RegularExpressions.Regex.Replace(phone, @"[\s\-\(\)\+]", "");
            
            // Check if only digits remain and length is between 10-15
            return System.Text.RegularExpressions.Regex.IsMatch(cleanPhone, @"^\d{10,15}$");
        }

        // ============================================
        // READ OPERATIONS
        // ============================================
        public Invoices GetInvoiceById(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new InvalidOperationException("Invalid invoice ID");
            
            return _invoiceRead.GetInvoice(invoiceId);
        }

        public List<Invoices> GetAllInvoices()
        {
            return _invoiceRead.GetAllInvoices();
        }
    }
}
