using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Contracts
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
