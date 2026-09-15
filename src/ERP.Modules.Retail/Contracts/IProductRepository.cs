using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Products_Crud.Model;

namespace Products_Crud.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Items>> GetAllAsync();
        Task<Items?> GetByIdAsync(int id);
        Task<Items> AddAsync(Items item);
        Task<Items> UpdateAsync(Items item);
        Task DeleteAsync(Items item);
    }
}