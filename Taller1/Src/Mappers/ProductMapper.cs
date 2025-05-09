using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;

namespace Taller1.Src.Mappers
{
    public class ProductMapper
    {
        public static ProductResponseDto ToProductResponseDto(Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Urls = product.Urls,
                Stock = product.Stock,
                Brand = product.Brand
            };
        }
        public static List<ProductResponseDto> ToProductResponseDtoList(IEnumerable<Product> products)
        {
            return products.Select(ToProductResponseDto).ToList();
        }
        public static Product FromCreateDtoToProduct(ProductDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                Urls = dto.Urls,
                Stock = dto.Stock,
                Brand = dto.Brand
            };
        }
    }
}