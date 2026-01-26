using Microsoft.EntityFrameworkCore;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Grahaq> Grahaqs { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Invoices CreatedAt default value
            modelBuilder.Entity<Invoices>()
                .Property(i => i.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Configure Customers CreatedAt default value
            modelBuilder.Entity<Customers>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
