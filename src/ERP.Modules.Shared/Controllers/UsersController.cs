using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Modules.Shared.Filters;

namespace ERP.Modules.Shared.Controllers
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
