using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DTOs;
using Products_Crud.DAL;
using System.Linq;
using System.Threading.Tasks;
using Erp.Dto.PurchaseService;
using Erp.Bl.PurchaseTopCards;

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