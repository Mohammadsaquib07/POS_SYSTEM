namespace Products_Crud.Modules.Retail.Contracts;

public sealed record RetailCustomerDto(
    int Id,
    string Name,
    string? Email,
    string? Phone,
    string? BillingAddress);
