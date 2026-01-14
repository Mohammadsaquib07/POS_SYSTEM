using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Models;

namespace Products_Crud.BL
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}