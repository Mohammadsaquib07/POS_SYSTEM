// using Erp.interfaces.SampleRepository;
// using Erp.Model.Entities.SampleProducts;
// using Microsoft.EntityFrameworkCore;
// using Products_Crud.DAL;

// namespace Erp.Bl.SampleProductImpl
// {
//     public class SampleProductImplementation:ISampleProductRepository
//     {
//         private readonly UserDbContext _context;
//         public SampleProductImplementation(UserDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<List<SampleProducts>> GetAllAsync()
//         {
//             return await _context.SampleProducts.AsNoTracking().ToListAsync();
//         }

//         public async Task<SampleProducts?> GetByIdAsync(int Id)
//         {
//             return await _context.SampleProducts.FindAsync(Id);
//         }

//         public async Task<SampleProducts> AddAsync(SampleProducts sampleproducts)
//         {
//             _context.SampleProducts.Add(sampleproducts);
//             await _context.SaveChangesAsync();
//             return sampleproducts;
//         }

//         public async Task<bool> UpdateAsync(int id,SampleProducts sampleProducts)
//         {
//             var existing = await _context.SampleProducts.FindAsync(id);
//             if(existing is null) return false;
//             existing.Name = sampleProducts.Name;
//             existing.Price = sampleProducts.Price;
//             existing.StockQuantity = sampleProducts.StockQuantity;
//             await _context.SaveChangesAsync();
//             return true;
//         }

//         public async Task<bool> DeleteAsync(int Id)
//         {
//             var existing = await _context.SampleProducts.FindAsync(Id);
//             if(existing is null) return false;
//             _context.SampleProducts.Remove(existing);
//             await _context.SaveChangesAsync();
//             return true;
//         }
//     }
// }