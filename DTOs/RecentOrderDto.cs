using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_Crud.DTOs
{
    public class RecentOrderDto
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public int Items { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
    }
}