namespace Erp.interfaces.PurchaseTabCards
{
    public interface IPurchaseCardsRepository
    {
        Task<int> GetTotalPurchasesThisMonthAsync();
        Task<int> GetPendingOrdersCountAsync();
        Task<int> GetUnpaidBillsCountAsync();
    }
}