using Bogus;

using Taller1.Src.Models;

namespace Taller1.Src.Data.Seeders
{
    public class ProductSeeder
    {
        public static List<Product> GenerateProducts(int quantity = 10)
        {
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
                .Generate(quantity);

            return products;

        }
    }
}