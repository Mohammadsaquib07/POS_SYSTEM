using Erp.Bl.CurrentTenant;
using Microsoft.AspNetCore.Http;
using System;

namespace Erp.Bl.CurrentTenantImplementation
{
    public class CurrentTenantService :ICurrentTenantService
    {
        public int CompanyId{get;}

        public CurrentTenantService(IHttpContextAccessor accessor)
        {
            var claim = accessor.HttpContext?.User?.FindFirst("CompanyId");
            CompanyId = claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}