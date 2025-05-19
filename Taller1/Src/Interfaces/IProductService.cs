using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;
using Taller1.Src.RequestHelpers;

namespace Taller1.Src.Interfaces
{
    public interface IProductService
    {
        Task<PagedList<Product>> GetPagedProductsAsync(ProductParams productParams);
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> CreateAsync(ProductDto dto);
        Task<Product?> UpdateAsync(int id, ProductDto dto);
        Task<string?> DeleteAsync(int id);
    }
}