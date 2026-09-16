using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Modules.Shared.Application;
using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Contracts;

namespace ERP.Modules.Shared.Controllers
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
