using Microsoft.AspNetCore.Mvc;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/dashboard/cards
        [HttpGet("cards")]
        public async Task<IActionResult> GetDashboardCards()
        {
            var data = await _dashboardService.GetTopCards();
            return Ok(data);
        }

        // GET: api/dashboard/recent-orders?take=10
        [HttpGet("recent-orders")]
        public async Task<IActionResult> GetRecentOrders(int take = 10)
        {
            if (take <= 0 || take > 100)
                return BadRequest("take must be between 1 and 100");

            var orders = await _dashboardService.GetRecentOrders(take);
            return Ok(orders);
        }
    }
}
