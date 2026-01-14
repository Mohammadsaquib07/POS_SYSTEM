using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products_Crud.DAL;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _service;
        public DashboardController(DashboardService dashboardServicesObj)
        {
            _service = dashboardServicesObj;
        }

        [HttpGet("cards")]
        public async Task<IActionResult> GetCards()
        {
            var data = await _service.GetTopCards(); //It calls the service
            return Ok(data); // Send 200 OK with the JSON data
        }
    }
}