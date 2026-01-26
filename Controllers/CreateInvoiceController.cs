using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.DTOs;
using Products_Crud.Model;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateInvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        public CreateInvoiceController(IInvoiceService iinvoiceService)
        {
            _invoiceService = iinvoiceService;
        }

        // --------- 1️⃣ Create Invoice with New Customer ----------
        [HttpPost("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                // Validate request object
                if (request == null)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Request body is required." });
                }

                // Validate items exist and are not empty
                if (request.Items == null || request.Items.Count == 0)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Please select at least one product to create an invoice." });
                }

                // Validate customer information
                if (request.Customer == null)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Customer information is required." });
                }

                int invoiceId = _invoiceService.CreateCustomerAndInvoice(request);
                return Ok(new { InvoiceId = invoiceId, Message = "Invoice created successfully with new customer!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "Validation Error", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }

        // --------- 2️⃣ Create Invoice with Existing Customer ----------
        [HttpPost("CreateInvoiceWithExistingCustomer")]
        public IActionResult CreateInvoiceWithExistingCustomer([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                // Validate request object
                if (request == null)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Request body is required." });
                }

                // Validate items exist and are not empty
                if (request.Items == null || request.Items.Count == 0)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Please select at least one product to create an invoice." });
                }

                // Validate CustomerId
                if (request.CustomerId == null || request.CustomerId <= 0)
                {
                    return BadRequest(new { Message = "Validation Error", Details = "Valid CustomerId is required for existing customer." });
                }

                int invoiceId = _invoiceService.CreateInvoice(request);
                return Ok(new { InvoiceId = invoiceId, Message = "Invoice created successfully with existing customer!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = "Validation Error", Details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }

        // --------- 3️⃣ Get Invoice by ID ----------
        [HttpGet("GetInvoice/{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            try
            {
                var invoice = _invoiceService.GetInvoiceById(id);
                if (invoice == null)
                    return NotFound(new { Message = "Invoice not found" });

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }

        // --------- 4️⃣ Get All Invoices ----------
        [HttpGet("GetAll")]
        public IActionResult GetAllInvoices()
        {
            try
            {
                var invoices = _invoiceService.GetAllInvoices();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }
    }
}
