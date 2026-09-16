using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Shared.Contracts;
using ERP.Modules.Shared.DTOs;
using ERP.Modules.Shared.Application;

namespace ERP.Modules.Shared.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISharedDbContext _context;
        private readonly JwtService _jwt;

        public LoginController(ISharedDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await _context.Users
                .Include(u => u.Company)
                .SingleOrDefaultAsync(u =>
                    u.Username == request.Username &&
                    u.Company.Name == request.CompanyName);

            if (user == null)
                return Unauthorized("Invalid credentials");

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.Username, user.CompanyId);

            return Ok(new { Token = token });
        }
    
    }
}