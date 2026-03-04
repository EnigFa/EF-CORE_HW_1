using Microsoft.EntityFrameworkCore;
using HW_1.Models;

namespace HW_1.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)              
                .WithMany()                           
                .HasForeignKey(p => p.CategoryId)     
                .HasPrincipalKey(c => c.Id);         

           
        }
    }
}