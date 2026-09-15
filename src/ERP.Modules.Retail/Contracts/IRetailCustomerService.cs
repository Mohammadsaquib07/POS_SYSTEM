namespace Products_Crud.Modules.Retail.Contracts;

/// <summary>
/// Public Retail boundary for other modules that require customer data.
/// It deliberately exposes a contract DTO, never the Retail domain entity.
/// </summary>
public interface IRetailCustomerService
{
    Task<RetailCustomerDto?> GetCustomerAsync(int customerId, CancellationToken cancellationToken = default);
}
