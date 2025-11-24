using Microsoft.EntityFrameworkCore;
using MyBackend.Models;

namespace MyBackend.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        // public DbSet<UserRole> UserRoles { get; set; } // Join Entity
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Purchase> Purchases { get; set; }
        
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
                
                // Configure Unique Index
                modelBuilder.Entity<Purchase>()
                    .HasIndex(p => new { p.UserId, p.Date })
                    .IsUnique();
                
                modelBuilder.Entity<PurchaseProduct>()
                    .HasIndex(pi => new { pi.PurchaseId, pi.ProductId })
                    .IsUnique();
                
                // Configure PurchaseProduct Many-to-Many
                modelBuilder.Entity<PurchaseProduct>()
                    .HasKey(pi => new { pi.PurchaseId, pi.ProductId });
                
                modelBuilder.Entity<PurchaseProduct>()
                    .HasOne(pi => pi.Purchase)
                    .WithMany(p => p.PurchaseProducts)
                    .HasForeignKey(pi => pi.PurchaseId);
                
                modelBuilder.Entity<PurchaseProduct>()
                    .HasOne(pp => pp.Product)
                    .WithMany()
                    .HasForeignKey(pp => pp.ProductId);
        }
    }
}