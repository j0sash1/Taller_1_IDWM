using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class StoreContext(DbContextOptions<StoreContext> options) : IdentityDbContext<User>(options)
    {
        public required DbSet<Product> Products { get; set; }
        
        public required DbSet<ShippingAddres> ShippingAddres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                    .HasOne(u => u.ShippingAddres)
                    .WithOne(sa => sa.User)
                    .HasForeignKey<ShippingAddres>(sa => sa.UserId);
            List<IdentityRole> roles = 
            [
                new IdentityRole { Id = "1",Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2",Name = "User", NormalizedName = "USER"}
            ];

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}