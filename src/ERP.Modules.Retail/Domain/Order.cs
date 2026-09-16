using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using ERP.Modules.Shared.Contracts;

namespace ERP.Modules.Retail.Domain
{
  public class Order:ITenantEntity
  {
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public int CustomerId { get; set; }
    public ICollection<OrderItem>? Items { get; set; }
    public int CompanyId { get; set; } 
  }
}
