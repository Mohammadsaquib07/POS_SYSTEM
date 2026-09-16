namespace ERP.Modules.Retail.DTOs
{
    public class PurchaseCardSummaryDto
    {
        public int TotalPurchasesThisMonth { get; set; }
        public int PendingOrdersCount { get; set; }
        public int UnpaidBillsCount { get; set; }
    }
}