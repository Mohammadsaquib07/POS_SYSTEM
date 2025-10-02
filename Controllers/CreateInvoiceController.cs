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
        [HttpPost("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody] FullInvoiceRequest FullInvoiceRequestObj)
        {
            try
            {
                if (FullInvoiceRequestObj == null)
                {
                    return BadRequest("Invoice must have at least one item.");
                }
                if (FullInvoiceRequestObj.IsNewCustomer == true)
                {
                    int invoiceId= _invoiceService.CreateCustomerAndInvoice(FullInvoiceRequestObj);
                    return Ok(new { InvoiceId = invoiceId, Message = "Invoice created successfully!" });
                }
                else
                {
                    int newInvoiceId = _invoiceService.CreateInvoice(FullInvoiceRequestObj.Invoice);
                    return Ok(new { InvoiceId = newInvoiceId, Message = "Invoice created successfully!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Details = ex.Message });
            }

        }
    }
}
