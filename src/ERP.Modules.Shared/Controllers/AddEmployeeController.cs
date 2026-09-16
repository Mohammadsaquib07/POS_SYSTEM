// using Microsoft.AspNetCore.Mvc;
// using ERP.Modules.Shared.Domain;
// using ERP.Modules.Shared.Contracts;

// [ApiController]
// [Route("api/[controller]")]
// public class AddEmployeeController : ControllerBase
// {
//         private readonly IEmployeeRepository _employeeRepository;

//     public AddEmployeeController(IEmployeeRepository employeeRepository)
//     {
//         _employeeRepository = employeeRepository;
//     }

//     [HttpPost("Add")]
//     public IActionResult AddEmployee([FromBody] Employee emp)
//     {
//         if (emp == null) return BadRequest("Invalid data");
//         _employeeRepository.AddEmployee(emp);
//         return Ok(new { message = "Employee added successfully!" });

//     }
// }
