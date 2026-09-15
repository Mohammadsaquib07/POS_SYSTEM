using Erp.Bl.PurchaseTopCards;
using Erp.Dtos.PurchaseCardsDtos;
using Erp.interfaces.PurchaseTabCards;

namespace Erp.Bl.PurchaseCardsDto.Concrete
{
    public class PurchaseCardService:IPurchaseCardService
    {
        private readonly IPurchaseCardsRepository _PurchaseCardsRepository;

        public PurchaseCardService(IPurchaseCardsRepository PurchaseCardsRepositorys)
        {
            _PurchaseCardsRepository = PurchaseCardsRepositorys;
        }
        

        public async Task<PurchaseCardSummaryDto> GetPurchaseCardsSummary()
        {
            return new PurchaseCardSummaryDto
            {
                TotalPurchasesThisMonth = await _PurchaseCardsRepository.GetPendingOrdersCountAsync(),
                PendingOrdersCount = await _PurchaseCardsRepository.GetPendingOrdersCountAsync(),
                UnpaidBillsCount = await _PurchaseCardsRepository.GetUnpaidBillsCountAsync()
            };
        }
    }
}