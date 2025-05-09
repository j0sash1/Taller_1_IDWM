using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
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
        public Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
        }
    }
}