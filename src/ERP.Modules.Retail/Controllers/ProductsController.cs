using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IPurchaseInvoiceService _purchaseservice;

    public ProductsController(IPurchaseInvoiceService purchaseservice)
    {
        _purchaseservice = purchaseservice;
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetPurchaseById(int Id)
    {
        if (Id <= 0)
        {
            return BadRequest(new { message = "Invalid Invoice Id" });
        }
        var invoice = await _purchaseservice.GetByIdAsync(Id);

        if (invoice == null)
        {
            return NotFound(new { message = $"Invoice with Id:{Id} not found" });
        }
        return Ok(invoice);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int Id)
    {
        if (Id <= 0)
        {
            return BadRequest(new { message = "Invalid invoice id" });
        }

        var result = await _purchaseservice.DeleteAsync(Id);

        if (!result.Success)
        {
            
            return NotFound(new { message = result.Message });
        }

        return NoContent();
    }
}
