using System.Reflection;
using Erp.Bl.CurrentTenant;
using Erp.interfaces.Tenant;
using Erp.Model.Entities;
using Erp.Model.Entities.variantsproducts;

// using Erp.Model.Entities.SampleProducts;
using Erp.Model.PuchaseInvoicEntities;
using Erp.Model.PurchaseInvoiceItemEntities;
using Microsoft.EntityFrameworkCore;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class UserDbContext : DbContext
    {
        private readonly ICurrentTenantService _tenant;
        public DbSet<User> Users { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Grahaq> Grahaqs { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ProductsList> ProductsList { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        // public DbSet<SampleProducts> SampleProducts=>Set<SampleProducts>();

        public UserDbContext(DbContextOptions<UserDbContext> options, ICurrentTenantService iCurrentTenantService) : base(options)
        {
            _tenant = iCurrentTenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoices>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<Customers>()
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

        private static readonly MethodInfo SetGlobalQueryMethod =
         typeof(UserDbContext).GetMethod(nameof(SetGlobalQuery), BindingFlags.NonPublic | BindingFlags.Instance)!;
        private void SetGlobalQuery<T>(ModelBuilder builder) where T : class, ITenantEntity
        {
            builder.Entity<T>().HasQueryFilter(e => EF.Property<int>(e, "CompanyId") == _tenant.CompanyId);
        }
    }
}