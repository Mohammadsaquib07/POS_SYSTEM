using Erp.interfaces.SupplierRepo;
using Microsoft.EntityFrameworkCore;
using Erp.Model.Entities;
using ERP.Modules.Retail.Contracts;

namespace Erp.Dal.SupplierImplementation
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