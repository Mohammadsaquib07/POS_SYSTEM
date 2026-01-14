using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products_Crud.Model
{
  public class Order
  {
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
  }
}