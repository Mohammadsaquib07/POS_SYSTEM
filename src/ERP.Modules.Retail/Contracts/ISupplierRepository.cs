using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Contracts
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<int> CreateAsync(Supplier supplier);
    }
}
