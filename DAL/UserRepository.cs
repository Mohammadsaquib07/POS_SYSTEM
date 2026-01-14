//using Microsoft.EntityFrameworkCore;
//using MyApp.Data;
//using MyApp.Models;
//using Products_Crud.Interfaces;

//namespace Products_Crud.DAL
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public UserRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<User?> GetByIdAsync(int id)
//        {
//            // LINQ to filter by id
//            return await _context.Users
//                .Where(u => u.Id == id)
//                .FirstOrDefaultAsync();
//        }

//        public async Task<IEnumerable<User>> GetAllAsync()
//        {
//            // LINQ to enumerate all users
//            return await _context.Users
//                .AsNoTracking()
//                .ToListAsync();
//        }

//        public async Task<User> CreateAsync(User user)
//        {
//            // Add and persist
//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();
//            return user;
//        }

//        public async Task UpdateAsync(User user)
//        {
//            // Find existing using LINQ, update fields and save
//            var existing = await _context.Users
//                .Where(u => u.Id == user.Id)
//                .FirstOrDefaultAsync();

//            if (existing is null)
//                return; // or throw if you prefer

//            existing.FirstName = user.FirstName;
//            existing.Email = user.Email;
//            existing.Password = user.Password;
//            existing.Gender = user.Gender;
//            await _context.SaveChangesAsync();
//        }
//        public async Task DeleteAsync(User user)
//        {
//            // Find by id using LINQ then remove
//            var existing = await _context.Users
//                .Where(u => u.Id == user.Id)
//                .FirstOrDefaultAsync();

//            if (existing is null)
//                return; // or throw if you prefer

//            _context.Users.Remove(existing);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
