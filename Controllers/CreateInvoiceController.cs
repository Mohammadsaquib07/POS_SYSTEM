using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.DTOs;

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

        // --------- Create Invoice (new or existing customer) ----------
        [HttpPost("CreateInvoice")]
        public async System.Threading.Tasks.Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { Success = false, Message = "Request body is required.", Details = "Please provide valid invoice data." });
                }

                if (request.Items == null || request.Items.Count == 0)
                {
                    return BadRequest(new { Success = false, Message = "Please add at least one product to the invoice." });
                }

                if (request.IsNewCustomer)
                {
                    if (request.Customer == null)
                    {
                        return BadRequest(new { Success = false, Message = "Customer information is required for a new customer." });
                    }

                    if (string.IsNullOrWhiteSpace(request.Customer.Name) || string.IsNullOrWhiteSpace(request.Customer.Email))
                    {
                        return BadRequest(new { Success = false, Message = "Customer name and email are required for a new customer." });
                    }
                }
                else
                {
                    if (request.CustomerId == null || request.CustomerId <= 0)
                    {
                        return BadRequest(new { Success = false, Message = "Please provide a valid CustomerId for an existing customer." });
                    }
                }

                var invoiceResponse = await _invoiceService.CreateInvoiceAsync(request);
                return Ok(new
                {
                    Success = true,
                    Data = invoiceResponse,
                    Message = "Invoice created successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        // --------- 3️⃣ Get Invoice by ID ----------
        [HttpGet("GetInvoice/{id}")]
        public async System.Threading.Tasks.Task<IActionResult> GetInvoiceById(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
                if (invoice == null)
                    return NotFound(new { Message = "Invoice not found" });

                return Ok(new
                {
                    Success = true,
                    Data = invoice
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }

        // --------- 4️⃣ Get All Invoices ----------
        [HttpGet("GetAll")]
        public async System.Threading.Tasks.Task<IActionResult> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoicesAsync();
                return Ok(new
                {
                    Success = true,
                    Data = invoices
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }
        }
    }
}
