using Microsoft.EntityFrameworkCore;
using ShoppingCart_Backend.Model;

namespace ShoppingCart_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }  
        public DbSet<Order> orders { get; set; }
        public DbSet<Item> items { get; set; }
    }
}
