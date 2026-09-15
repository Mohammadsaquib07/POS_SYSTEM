using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Products_Crud.Model;
using System.Threading.Tasks;

namespace ERP.ERP.Modules.Shared
{
    public interface ISharedDbContext
    {
        DbSet<User> Users { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync();
    }
}