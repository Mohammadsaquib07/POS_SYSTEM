using Erp.Dtos.PurchaseCardsDtos;

namespace Erp.Bl.PurchaseTopCards
{
    public interface IPurchaseCardService
    {
        Task<PurchaseCardSummaryDto> GetPurchaseCardsSummary();
    }
}