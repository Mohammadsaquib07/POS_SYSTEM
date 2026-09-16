using System.Reflection;
using ERP.Modules.Retail.Domain;
using ERP.Modules.Retail.Application;
using ERP.Modules.Retail.Contracts;
using ERP.Modules.Retail.Controllers;
using ERP.Modules.Retail.DTOs;
using ERP.Modules.Retail.Infrastructure;
using ERP.Modules.Retail.Enums;
using Microsoft.EntityFrameworkCore;
using ERP.Modules.Shared.Contracts;
using ERP.Modules.Shared.Domain;

namespace Products_Crud.DAL
{
     public class UserDbContext : DbContext, ISharedDbContext, IRetailDbContext
    {
        private readonly ICurrentTenantService _tenant;

        public DbSet<User> Users { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<Company> Companies { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options, ICurrentTenantService iCurrentTenantService) : base(options)
        {
            _tenant = iCurrentTenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Shared, tenant-wide identities remain in the shared schema.
            modelBuilder.Entity<Company>().ToTable("Companies", "shared");
            modelBuilder.Entity<User>().ToTable("Users", "shared");

            // Retail owns all sales, catalog, supplier, and purchase records.
            modelBuilder.Entity<Items>().ToTable("Items", "retail");
            modelBuilder.Entity<Product>().ToTable("Products", "retail");
            modelBuilder.Entity<ProductVariant>().ToTable("ProductVariants", "retail");
            modelBuilder.Entity<Customer>().ToTable("Customers", "retail");
            modelBuilder.Entity<Order>().ToTable("Orders", "retail");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems", "retail");
            modelBuilder.Entity<Invoice>().ToTable("Invoices", "retail");
            modelBuilder.Entity<InvoiceItem>().ToTable("InvoiceItems", "retail");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers", "retail");
            modelBuilder.Entity<PurchaseInvoice>().ToTable("PurchaseInvoices", "retail");
            modelBuilder.Entity<PurchaseInvoiceItem>().ToTable("PurchaseInvoiceItems", "retail");

            modelBuilder.Entity<Invoice>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Customer>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<PurchaseInvoice>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchaseInvoice>()
                .HasOne(pi => pi.Supplier)
                .WithMany(s => s.PurchaseInvoices)
                .HasForeignKey(pi => pi.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(item => item.PurchaseInvoice)
                .WithMany(pi => pi.Items)
                .HasForeignKey(item => item.PurchaseInvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseInvoiceItem>()
                .HasOne(item => item.Product)
                .WithMany()
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = SetGlobalQueryMethod.MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        // Explicit interface implementation for SaveChangesAsync
        async Task<int> ISharedDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        async Task<int> IRetailDbContext.SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        private static readonly MethodInfo SetGlobalQueryMethod =
         typeof(UserDbContext).GetMethod(nameof(SetGlobalQuery), BindingFlags.NonPublic | BindingFlags.Instance)!;
        private void SetGlobalQuery<T>(ModelBuilder builder) where T : class, ITenantEntity
        {
            builder.Entity<T>().HasQueryFilter(e => EF.Property<int>(e, "CompanyId") == _tenant.CompanyId);
        }
    }
}
