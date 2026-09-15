using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_Crud.DTOs
{
    public class DashboardCardsDto
    {
        public decimal TodayslSales {get;set;}
        public int TotalOrders {get;set;}
        public decimal AvgOrderValue {get;set;}
        public string?TopProduct { get; set; }

    }
}