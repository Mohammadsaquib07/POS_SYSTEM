//using Microsoft.AspNetCore.Mvc;
//using Products_Crud.DAL;

//namespace Products_Crud.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class EmployeeController : ControllerBase
//    {
//       private readonly EmpRepository _empRepository;
//        public EmployeeController(EmpRepository empRepository) {
//            _empRepository = empRepository;
//        }

//        [HttpGet("Get")]
//        public IActionResult GetAllEmployees()
//        {
//            var products = _empRepository.GetEmployeeData();
//            return Ok(products);
//        }

//        [HttpGet]
//        public IEnumerable<string> getList()
//        {
//            return new string[] { "Product1", "Product2" };
//        }
//    }
//}
