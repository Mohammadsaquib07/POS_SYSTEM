using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DTOs;
using Products_Crud.Model;
using Products_Crud.Services;

namespace Products_Crud.DAL
{
    public class UserService
    {
        private readonly UserDbContext _context;
        public UserService(UserDbContext cntxtObj)
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
