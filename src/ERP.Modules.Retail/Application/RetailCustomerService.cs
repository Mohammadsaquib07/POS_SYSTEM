using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Application;

internal sealed class RetailCustomerService(IRetailDbContext dbContext) : IRetailCustomerService
{
    public Task<RetailCustomerDto?> GetCustomerAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Customers
            .AsNoTracking()
            .Where(customer => customer.CustomerId == customerId)
            .Select(customer => new RetailCustomerDto(
                customer.CustomerId,
                customer.Name,
                customer.Email,
                customer.Phone,
                customer.BillingAddress))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
