using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erp.interfaces.Tenant;

namespace Products_Crud.Model
{
  public class Order:ITenantEntity
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public int CustomerId { get; set; }
    public Grahaq? Grahaqs { get; set; }
    public ICollection<OrderItem>? Items { get; set; }
    public int CompanyId { get; set; } 
  }
}