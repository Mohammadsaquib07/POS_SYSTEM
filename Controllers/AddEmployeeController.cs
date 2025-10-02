using Microsoft.AspNetCore.Mvc;
using Products_Crud.Model;

[ApiController]
[Route("api/[controller]")]
public class AddEmployeeController : ControllerBase
{
    //bhai ye bata controller se o service(IEmployeeRepository) kaise call ho sakti hai iska kya scene hai
    //Ye exactly constructor-based dependency injection ka magic hai jo .NET Core automatic karta hai — aur ye hi interviewers ko bhi sun’na pasand hai. jo ki program.cs me register karne ke baad wanse instance milta hai.
    private readonly IEmployeeRepository _employeeRepository;

    public AddEmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpPost("Add")]
    public IActionResult AddEmployee([FromBody] Employees emp)
    {
        if (emp == null) return BadRequest("Invalid data");
        _employeeRepository.AddEmployee(emp);
        return Ok(new { message = "Employee added successfully!" });

    }
}