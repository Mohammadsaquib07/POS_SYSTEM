using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_Crud.DTOs
{
    public class RecentOrderDto
    {
        public string OrderId { get; set; } = null!;
        public string Customer { get; set; } = null!;
        public int Items { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null!;
    }
}