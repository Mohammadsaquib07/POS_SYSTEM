// src/ERP.Modules.Retail/Contracts/IRetailDbContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Products_Crud.Model;
using Erp.Model.Entities;
using Erp.Model.PuchaseInvoicEntities;

namespace ERP.Modules.Retail.Contracts
{
    public interface IRetailDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<InvoiceItem> InvoiceItems { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Supplier> Suppliers { get; }
        DbSet<PurchaseInvoice> PurchaseInvoices { get; }
        DbSet<Items> Items { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync();
    }
}