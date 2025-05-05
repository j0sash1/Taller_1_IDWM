using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using Microsoft.EntityFrameworkCore;

using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<StoreContext>()
                ?? throw new InvalidOperationException("Could not get StoreContext");
context.Database.Migrate();
            SeedUsers(context);
            SeedProducts(context);
        }

        private static void SeedUsers(StoreContext context)
        {
            if (context.Users.Any()) return;

            var faker = new Faker("es");

            var users = new Faker<User>()
                .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.LastName, f => f.Person.LastName)
                .RuleFor(u => u.Thelephone, f => f.Phone.PhoneNumber())
                .Generate(10);

            foreach (var user in users)
            {
                var shippingAddres = new ShippingAddres
                {
                    Street = faker.Address.StreetName(),
                    Number = faker.Address.BuildingNumber(),
                    Commune = faker.Address.City(),
                    Region = faker.Address.State(),
                    PostalCode = faker.Address.ZipCode(),
                    User = user
                };
                user.ShippingAddres = shippingAddres;
            }

            context.Set<User>().AddRange(users);
            context.SaveChanges();
        }

        private static void SeedProducts(StoreContext context)
        {

            if (context.Products.Any()) return;

            var faker = new Faker("es");

            var products = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Category, f => f.Commerce.Department())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => f.Random.Decimal(5000, 50000))
                .RuleFor(p => p.Brand, f => f.Company.CompanyName())
                .RuleFor(p => p.Stock, f => f.Random.Int(10, 200))
                .RuleFor(p => p.Urls, f => new[]
                {
                    $"https://res.cloudinary.com/demo/image/upload/sample1.jpg",
                    $"https://res.cloudinary.com/demo/image/upload/sample2.jpg",
                    $"https://res.cloudinary.com/demo/image/upload/sample3.jpg"
                })
                .Generate(10);

           

            context.Set<Product>().AddRange(products);
            context.SaveChanges();
        }
    }
}