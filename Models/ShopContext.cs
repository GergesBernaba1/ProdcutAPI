using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportAPI.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catgory>().HasMany(c => c.Products).WithOne(a => a.Catgory).HasForeignKey(a => a.CatgoryId);
            modelBuilder.Entity<Order>().HasMany(o => o.Products);
            modelBuilder.Entity<Order>().HasOne(o => o.User);
            modelBuilder.Entity<User>().HasMany(u => u.Order).WithOne(o => o.User).HasForeignKey(o => o.UserId);

            modelBuilder.Seed();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Catgory> Catgories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
