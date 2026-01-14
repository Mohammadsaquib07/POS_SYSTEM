using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.BL;
using Products_Crud.Model;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(BL.IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student s)
        {
            await _studentService.AddStudent(s);

            return CreatedAtAction(nameof(GetStudent), new { id = s.Id }, s);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student s)
        {
            if (id != s.Id)
            {
                return BadRequest();
            }
            await _studentService.UpdateStudent(s);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteStudent(id);
            return NoContent();
        }
    }
}
