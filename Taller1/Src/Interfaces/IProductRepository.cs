using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}