using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("test-exception")]
        public IActionResult Test()
        {
            throw new Exception("Something broke!");
        }

    }
}
