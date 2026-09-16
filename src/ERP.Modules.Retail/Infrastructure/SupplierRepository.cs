using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using Microsoft.EntityFrameworkCore;

namespace ERP.Modules.Retail.Infrastructure
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IRetailDbContext _userdbcontext;
        public SupplierRepository(IRetailDbContext userdbcontext)
        {
            _userdbcontext = userdbcontext;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _userdbcontext.Suppliers
            .Where(s => s.IsActive)
            .OrderBy(s => s.Name)
            .ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _userdbcontext.Suppliers.FindAsync(id);
        }

        public async Task<int> CreateAsync(Supplier supplier)
        {
            _userdbcontext.Suppliers.Add(supplier);
            await _userdbcontext.SaveChangesAsync();
            return supplier.Id;
        }
    }
}
