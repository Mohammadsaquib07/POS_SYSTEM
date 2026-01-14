using Microsoft.EntityFrameworkCore;
using Products_Crud.Model;

namespace Products_Crud.DAL
{
    public class UserDbContext: DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Items> Items{get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderItem> OrderItems {get;set;}
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
    }
}
