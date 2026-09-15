using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.Filters;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthorizationFilter))]
    public class UsersController : ControllerBase
    {
        [HttpGet("getall")]
        public IActionResult GetAllUsers()
        {
            return Ok(new[] { "John", "Jane", "Mike" });
        }
    }
}
