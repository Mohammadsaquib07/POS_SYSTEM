using Azure.Core;
using ERP.Modules.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Shared.DTOs;
using ERP.Modules.Shared.Domain;
using ERP.Modules.Shared.Application;

namespace ERP.Modules.Shared.Infrastructure
{
    public class UserService
    {
        private readonly ISharedDbContext _context;
        public UserService(ISharedDbContext cntxtObj)
        {
            _context = cntxtObj;
        }
        public async Task<bool> UserExists(string username, string email)
        {
            return await _context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        }
        public async Task<User> Register(RegisterRequestDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var userObj = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                CreatedDate = DateTime.UtcNow
            };
            _context.Users.Add(userObj);
            await _context.SaveChangesAsync();
            return userObj;
        }
    }
}
