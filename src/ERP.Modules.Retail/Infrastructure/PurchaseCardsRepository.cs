using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using Microsoft.EntityFrameworkCore;

namespace ERP.Modules.Retail.Infrastructure
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
