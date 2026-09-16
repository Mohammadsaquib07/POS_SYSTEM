namespace ERP.Modules.Retail.Contracts
{
    public interface IPurchaseCardsRepository
    {
        Task<int> GetTotalPurchasesThisMonthAsync();
        Task<int> GetPendingOrdersCountAsync();
        Task<int> GetUnpaidBillsCountAsync();
    }
}