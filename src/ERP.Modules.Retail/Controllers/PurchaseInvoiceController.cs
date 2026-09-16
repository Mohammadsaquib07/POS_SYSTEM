using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using Microsoft.AspNetCore.Mvc;

// One model for the whole multipart body — the JSON payload as a plain
// string field, plus the optional file. This is what fixes the Swagger
// generation crash: Swashbuckle needs a single bound model, not two
// separate [FromForm] parameters, when one of them is an IFormFile.
public class CreatePurchaseFormRequest
{
    public string Purchase { get; set; } = null!;
    public IFormFile? Attachment { get; set; }
}

[ApiController]
[Route("api/purchase-invoices")]
public class PurchaseInvoiceController : ControllerBase
{
    private readonly IPurchaseInvoiceService _service;
    private static readonly string[] AllowedAttachmentTypes = { "image/jpeg", "image/png", "image/webp", "application/pdf" };
    private const long MaxAttachmentBytes = 5 * 1024 * 1024;

    public PurchaseInvoiceController(IPurchaseInvoiceService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _service.GetAllAsync();
        return Ok(invoices);
    }

    // The Angular side now ALWAYS sends multipart/form-data for this endpoint
    // (a plain field named "purchase" holding the JSON, plus an optional
    // "attachment" file) rather than switching between JSON and multipart
    // depending on whether a file was picked. One content type, one action,
    // much simpler than trying to bind two different request shapes here.
    // NOTE: bound as a single wrapper model (CreatePurchaseFormRequest)
    // instead of two loose [FromForm] parameters — Swashbuckle can't
    // generate Swagger docs for an action with a bare [FromForm] IFormFile
    // parameter. Just as important: the parameter below has NO [FromForm]
    // attribute on it. ASP.NET Core already infers "this comes from the
    // form" on its own once it sees a model containing an IFormFile —
    // adding [FromForm] explicitly is redundant, and that redundant
    // attribute is specifically what was still crashing Swagger generation.
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(CreatePurchaseFormRequest request)
    {
        var attachment = request.Attachment;
        CreatePurchaseInvoiceDto? dto;
        try
        {
            dto = JsonSerializer.Deserialize<CreatePurchaseInvoiceDto>(
                request.Purchase,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch
        {
            return BadRequest(new { Message = "Purchase data is not valid JSON." });
        }

        if (dto == null)
        {
            return BadRequest(new { Message = "Purchase data is required." });
        }

        string? attachmentPath = null;

        if (attachment != null && attachment.Length > 0)
        {
            if (!AllowedAttachmentTypes.Contains(attachment.ContentType))
            {
                return BadRequest(new { Message = "Only JPG, PNG, WEBP or PDF files are allowed." });
            }
            if (attachment.Length > MaxAttachmentBytes)
            {
                return BadRequest(new { Message = "Attachment must be under 5MB." });
            }

            var uploadsFolder = Path.Combine("wwwroot", "uploads", "purchase-bills");
            Directory.CreateDirectory(uploadsFolder);

            var safeFileName = $"{Guid.NewGuid()}{Path.GetExtension(attachment.FileName)}";
            var fullPath = Path.Combine(uploadsFolder, safeFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await attachment.CopyToAsync(stream);
            }

            attachmentPath = $"/uploads/purchase-bills/{safeFileName}";
        }

        var results = await _service.CreateAsync(dto, attachmentPath);

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

    // NEW — routes through the service, not the repository, so stock and
    // cost price get reversed correctly instead of silently drifting.
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
