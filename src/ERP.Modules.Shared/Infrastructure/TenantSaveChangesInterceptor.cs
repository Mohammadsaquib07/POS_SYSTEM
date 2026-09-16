using Microsoft.EntityFrameworkCore.Diagnostics;
using ERP.Modules.Shared.Contracts;
using ERP.Modules.Shared.Contracts;

namespace Erp.Bl.TenantSaveChanges
{
    public class TenantSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentTenantService _tenant;
        public TenantSaveChangesInterceptor(ICurrentTenantService tenant) => _tenant = tenant;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return result;

            foreach (var entry in context.ChangeTracker.Entries<ITenantEntity>())
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    entry.Entity.CompanyId = _tenant.CompanyId;
            }
            return result;
        }
    }
}
