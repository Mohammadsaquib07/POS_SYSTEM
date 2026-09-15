using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Contracts;
using Products_Crud.Modules.Retail.Contracts;

namespace Products_Crud.Modules.Retail.Application;

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
