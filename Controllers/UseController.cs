using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UseController : ControllerBase
    {
        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            int a = 10;
            int b = 0; // variable, not constant
            int x = a / b; // now runtime exception
            return Ok();
        }
    }
}
