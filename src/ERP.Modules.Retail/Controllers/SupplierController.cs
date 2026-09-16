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
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService supplierService;

        public SupplierController(ISupplierService supplierinstance)
        {
            supplierService = supplierinstance;
        }


        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var supplier = await supplierService.GetAllAsync();
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto)
        {
            var result = await supplierService.CreateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(Request); 
        }
    }
}
