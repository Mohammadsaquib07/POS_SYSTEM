using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Modules.Shared.Infrastructure;
using ERP.Modules.Shared.DTOs;

namespace ERP.Modules.Shared.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthSharedController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthSharedController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            if (await _userService.UserExists(dto.Username, dto.Email))
            {
                return BadRequest("User with given username or email already exists.");
            }
            var user = await _userService.Register(dto);
            return Ok(new {
                message = "User registered successfully",
                user.Id, user.Username, user.Email, user.CreatedDate
            });
        }
    }
}