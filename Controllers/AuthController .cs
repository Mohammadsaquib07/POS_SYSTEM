using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products_Crud.DAL;
using Products_Crud.DTOs;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            // Check if user exists
            if (await _userService.UserExists(dto.Username, dto.Email))
            {
                return BadRequest("User with given username or email already exists.");
            }
            var user = await _userService.Register(dto);
            return Ok(new {
                message = "User registered successfully",
                user.Id, user.Username, user.Email, user.CreatedAt
            });
        }
    }
}