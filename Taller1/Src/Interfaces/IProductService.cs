using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;

namespace Taller1.Src.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetAllProductsAsync();
        Task<ProductResponseDto?> GetProductByIdAsync(int id);
        Task<ProductResponseDto> CreateProductAsync(ProductDto productDto);
        Task<ProductResponseDto?> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}