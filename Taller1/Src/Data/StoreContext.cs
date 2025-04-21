using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Product> Products { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<ShippingAddres> ShippingAddres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<User>()
             .HasOne(u => u.ShippingAddres)
             .WithOne(sa => sa.User)
             .HasForeignKey<ShippingAddres>(sa => sa.UserId)
             .OnDelete(DeleteBehavior.Cascade);
   
        }
    }
}