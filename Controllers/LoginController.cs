using Microsoft.AspNetCore.Mvc;
using Products_Crud.DAL;
using Products_Crud.DTOs;
using Products_Crud.Services;

namespace Products_Crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly JwtService _jwt;

        public LoginController(UserDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Invalid credentials");

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.Username);

            return Ok(new { Token = token });
        }
    }
}
