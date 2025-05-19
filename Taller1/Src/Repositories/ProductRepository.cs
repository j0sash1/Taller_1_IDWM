using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly StoreContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(StoreContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            // Retorna null si no encuentra el producto
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public IQueryable<Product> GetQueryableProducts()
        {
            return _context.Products.AsQueryable();
        }

        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task<bool> IsProductInOrdersAsync(int productId)
        {
            return await _context.OrderItems.AnyAsync(i => i.ProductId == productId);
        }

    }
}