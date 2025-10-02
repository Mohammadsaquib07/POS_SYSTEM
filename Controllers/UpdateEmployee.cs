using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.Model;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateEmployeeRecord : ControllerBase
    {
        private readonly IEmployeeUpdateService _IEmployeeUpdateService;
        public UpdateEmployeeRecord(IEmployeeUpdateService IEmployeeUpdateServiceObj)
        {
            _IEmployeeUpdateService = IEmployeeUpdateServiceObj;
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employees emp)
        {

            if (emp == null || id != emp.Eid)
                return BadRequest();
            _IEmployeeUpdateService.UpdateEmployeeData(id, emp);
            return Ok(new { message = "Employee updated successfully!" });
        }

    }
}
