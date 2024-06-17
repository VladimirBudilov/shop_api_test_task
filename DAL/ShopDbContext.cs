using DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class ShopDbContext : DbContext
{
        public ShopDbContext(DbContextOptions<ShopDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
}