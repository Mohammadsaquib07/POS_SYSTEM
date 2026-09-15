using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.Model;
using Products_Crud.Common.Contracts;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly IEmployeeDeleteRepository _employeeDeleteRepository;
        public DeleteController(IEmployeeDeleteRepository IEmployeeDeleteRepositoryObj)
        {
            _employeeDeleteRepository = IEmployeeDeleteRepositoryObj;
        }

        [HttpPost("Delete/{id}")]
        public IActionResult DeleteEmployee(int id, [FromBody] Employee emp)
        {
            if (emp == null || id != emp.Eid)
                return BadRequest();
            _employeeDeleteRepository.DeleteEmployee(id);
            return Ok(new { message = "Employee Deleted successfully!" });
        }
    }
}
