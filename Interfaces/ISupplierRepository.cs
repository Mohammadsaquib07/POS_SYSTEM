using Erp.Model.Entities;

namespace Erp.interfaces.SupplierRepo{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<int> CreateAsync(Supplier supplier);
    }
}