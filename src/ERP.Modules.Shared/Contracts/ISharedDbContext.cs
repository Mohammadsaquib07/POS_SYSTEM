using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ERP.Modules.Shared.Domain;
using System.Threading.Tasks;

namespace ERP.Modules.Shared.Contracts
{
    public interface ISharedDbContext
    {
        DbSet<User> Users { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync();
    }
}