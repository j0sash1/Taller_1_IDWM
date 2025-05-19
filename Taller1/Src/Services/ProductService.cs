/**
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Services
{
    public class ProductService : IProductService
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetProductsAsync();
            return ProductMapper.ToProductResponseDtoList(products);
        }
        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (product == null)
                throw new Exception("Producto no encontrado");

            return ProductMapper.ToProductResponseDto(product);
        }
        public async Task<ProductResponseDto> CreateProductAsync(ProductDto dto)
        {
            var product = ProductMapper.FromCreateDtoToProduct(dto);

            await _unitOfWork.ProductRepository.AddProductAsync(product);
            await _unitOfWork.SaveChangeAsync();

            return ProductMapper.ToProductResponseDto(product);
        }
        public async Task<ProductResponseDto> UpdateProductAsync(int id, ProductDto dto)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (product == null)
                throw new Exception("Producto no encontrado");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            product.Urls = dto.Urls;
            product.Stock = dto.Stock;
            product.Brand = dto.Brand;

            await _unitOfWork.SaveChangeAsync();

            return ProductMapper.ToProductResponseDto(product);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (product == null) return false;

            _unitOfWork.ProductRepository.DeleteProductAsync(product);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
**/