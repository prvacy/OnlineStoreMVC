using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCTest.Models.Product;
using MVCTest.Models.User;


namespace MVCTest.Models
{
    public class SiteDbContext : DbContext
    {
        public DbSet<Product.Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User.User> Users { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public SiteDbContext(DbContextOptions<SiteDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(i => i.IsSubmitted).HasDefaultValue(false);
        }

    }
}
