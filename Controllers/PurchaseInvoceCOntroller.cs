using Erp.Dto.PurchaseService;
using Erp.Dtos.CreatePurchase;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/purchase-invoices")]
public class PurchaseInvoiceController : ControllerBase
{
    private readonly IPurchaseInvoiceService _service;
    public PurchaseInvoiceController(IPurchaseInvoiceService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _service.GetAllAsync();
        return Ok(invoices);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseInvoiceDto dto)
    {
        var results = await _service.CreateAsync(dto);
        if (results == null || results.Count == 0)
            return BadRequest(new { Message = "No response from service." });

        if (results.Count == 1)
        {
            var single = results[0];
            return single.Success ? Ok(single) : BadRequest(single);
        }
        if (results.Any(r => !r.Success))
            return BadRequest(results);

        return Ok(results);
    }
}