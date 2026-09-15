using Erp.Bl.CurrentTenant;
using Erp.interfaces.Tenant;
using Microsoft.EntityFrameworkCore.Diagnostics;

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