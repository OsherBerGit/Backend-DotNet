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
        
        public DbSet<ProductReview> ProductReviews { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
                
                // Configure Unique Index
                modelBuilder.Entity<Purchase>()
                    .HasIndex(p => new { p.UserId, p.Date })
                    .IsUnique();
                
                // Configure PurchaseProduct Many-to-Many
                modelBuilder.Entity<PurchaseProduct>()
                    .HasKey(pp => new { pp.PurchaseId, pp.ProductId });
                
                modelBuilder.Entity<PurchaseProduct>()
                    .HasOne(pp => pp.Purchase)
                    .WithMany(p => p.PurchaseProducts)
                    .HasForeignKey(pp => pp.PurchaseId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                modelBuilder.Entity<PurchaseProduct>()
                    .HasOne(pp => pp.Product)
                    .WithMany() // no need to store the inverse
                    .HasForeignKey(pp => pp.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configure ProductReview Unique Index and Many-to-Many
                modelBuilder.Entity<ProductReview>()
                    .HasIndex(pr => new { pr.ProductId, pr.UserId })
                    .IsUnique();
                
                modelBuilder.Entity<ProductReview>()
                    .HasOne(pr => pr.Product)
                    .WithMany()
                    .HasForeignKey(pr => pr.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                modelBuilder.Entity<ProductReview>()
                    .HasOne(pr => pr.User)
                    .WithMany()
                    .HasForeignKey(pr => pr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}