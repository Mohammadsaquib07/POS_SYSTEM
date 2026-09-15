using Erp.interfaces.PurchaseTabCards;
using Products_Crud.Common.Enums;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Retail.Contracts;

namespace Erp.Dal.PurchaseCardsImplementation
{
    public class PurchaseCardsRepository:IPurchaseCardsRepository
    {
        private readonly IRetailDbContext _userDbContext;

        public PurchaseCardsRepository(IRetailDbContext userDbContexts)
        {
            _userDbContext = userDbContexts;
        }
        public async Task<int> GetTotalPurchasesThisMonthAsync()
        {
            var now = DateTime.UtcNow;
            return await _userDbContext.PurchaseInvoices
            .Where(p=>p.InvoiceDate.Month == now.Month && p.InvoiceDate.Year == now.Year)
            .CountAsync();
        }
        public async Task<int> GetPendingOrdersCountAsync()
        {
            return await _userDbContext.PurchaseInvoices
            .Where(p=>p.Status == PurchaseInvoiceStatus.Pending)
            .CountAsync();
        }
        public async Task<int> GetUnpaidBillsCountAsync(){
            return await _userDbContext.PurchaseInvoices
            .Where(p => p.Status == PurchaseInvoiceStatus.Unpaid)
            .CountAsync();
        }
    }
}
