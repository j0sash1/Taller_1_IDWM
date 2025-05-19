using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data.Seeders;
using Taller1.Src.Dtos;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>()
                ?? throw new InvalidOperationException("Could not get UserManager");

            var context = scope.ServiceProvider.GetRequiredService<StoreContext>()
                ?? throw new InvalidOperationException("Could not get StoreContext");

            await SeedData(context, userManager);
        }

        private static async Task SeedData(StoreContext context, UserManager<User> userManager)
        {
            await context.Database.MigrateAsync();

            if (!context.Products.Any())
            {
                var products = ProductSeeder.GenerateProducts(10);
                context.Products.AddRange(products);
            }
            if (!context.Users.Any())
            {
                var userDtos = UserSeeder.GenerateUserDtos(10);
                await UserSeeder.CreateUsers(userManager, userDtos);
            }

            await context.SaveChangesAsync();
        }
    }
}