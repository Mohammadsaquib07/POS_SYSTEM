using Microsoft.AspNetCore.Mvc;
using Erp.Bl.IsupplierInterface;
using Erp.Dto.Request.Dtos;
using Erp.Dto.supplierDtos;

namespace Products_Crud.Controllers
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