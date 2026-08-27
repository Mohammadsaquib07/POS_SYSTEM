using Microsoft.EntityFrameworkCore;
using Products_Crud.DAL;
using Products_Crud.Interfaces;
using Products_Crud.Model;

namespace Products_Crud.BL
{
    public class ProductRepository : IProductRepository
    {
        private readonly UserDbContext _context;

        public ProductRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Items>> GetAllAsync()
        {
            // return await _context.Items.ToListAsync();
             return await _context.Items.Include(i => i.Variants).ToListAsync();
        }

        public async Task<Items?> GetByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<Items> AddAsync(Items item)
        {
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Items> UpdateAsync(Items item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteAsync(Items item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
