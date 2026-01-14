using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Products_Crud.DTOs;

namespace Products_Crud.DAL
{
    public class DashboardService
    {
        private readonly UserDbContext _userDbContext;
        public DashboardService(UserDbContext _userDbContextObj)
        {
            _userDbContext = _userDbContextObj;
        }

       public async Task<DashboardCardsDto> GetTopCards()
        {
            var today = DateTime.Today;

            var todaysSales = await _userDbContext.Orders
            .Where(o=>o.OrderDate >= today)
            .SumAsync(o=>(decimal?)o.TotalAmount) ?? 0;

            var totalOrders = await _userDbContext.Orders.CountAsync();

            var avgValue = totalOrders > 0 ? await _userDbContext.Orders.AverageAsync(o=>o.TotalAmount) : 0;

            var topProduct = await _userDbContext.OrderItems
            .GroupBy(i=>i.ProductName)
            .OrderByDescending(g=>g.Sum(x=>x.Quantity))
            .Select(g=>g.Key)
            .FirstOrDefaultAsync() ?? "N/A";
            return new DashboardCardsDto
            {
              TodayslSales = todaysSales,
              TotalOrders = totalOrders,
              AvgOrderValue = avgValue,
              TopProduct = topProduct
            };
        }
    }
}