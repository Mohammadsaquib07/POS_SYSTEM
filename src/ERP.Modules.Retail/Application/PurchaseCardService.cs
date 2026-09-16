using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;

namespace ERP.Modules.Retail.Application
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
