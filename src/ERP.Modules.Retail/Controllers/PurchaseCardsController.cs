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
public class PurchaseCardsController : ControllerBase
{
    private readonly IPurchaseCardService _purchaseCardsservice;

    public PurchaseCardsController(IPurchaseCardService purchasecardservice)
    {
        _purchaseCardsservice = purchasecardservice;
    }

    [HttpGet("Summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result = await _purchaseCardsservice.GetPurchaseCardsSummary();
        return Ok(result);
    }

}
