using Taller1.Src.Dtos;
using Taller1.Src.Extensions;
using Taller1.Src.Helpers;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;
using Taller1.Src.RequestHelpers;
using Taller1.Src.Data;

namespace Taller1.Src.Services
{
    public class ProductService(UnitOfWork context, IPhotoService photoService) : IProductService
    {
        private readonly UnitOfWork _context = context;
        private readonly IPhotoService _photoService = photoService;

        public async Task<PagedList<Product>> GetPagedProductsAsync(ProductParams productParams)
        {
            var query = _context.ProductRepository.GetQueryableProducts()
                .Search(productParams.Search)
                .Filter(productParams.Brands, productParams.Categories)
                .Sort(productParams.OrderBy);

            return await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.ProductRepository.GetProductByIdAsync(id);
        }

        public async Task<Product?> CreateAsync(ProductDto dto)
        {
            var urls = new List<string>();
            string? publicId = null;

            foreach (var image in dto.Images)
            {
                var result = await _photoService.AddPhotoAsync(image);
                if (result.Error != null) return null;

                urls.Add(result.SecureUrl.AbsoluteUri);
                publicId ??= result.PublicId;
            }

            var product = ProductMapper.FromCreateDto(dto, urls, publicId);
            await _context.ProductRepository.AddProductAsync(product);
            await _context.SaveChangeAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, ProductDto dto)
        {
            var product = await _context.ProductRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            if (dto.Images.Any())
            {
                if (product.Urls != null && product.Urls.Any())
                {
                    foreach (var url in product.Urls)
                    {
                        var oldPublicId = CloudinaryHelper.ExtractPublicIdFromUrl(url);
                        if (!string.IsNullOrEmpty(oldPublicId))
                            await _photoService.DeletePhotoAsync(oldPublicId);
                    }
                }

                var result = await _photoService.AddPhotoAsync(dto.Images.First());
                if (result.Error != null) return null;

                product.Urls = new List<string> { result.SecureUrl.AbsoluteUri };
                product.PublicId = result.PublicId;
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Category = dto.Category;
            product.Brand = dto.Brand;
            product.Stock = dto.Stock;

            await _context.ProductRepository.UpdateProductAsync(product);
            await _context.SaveChangeAsync();

            return product;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            var product = await _context.ProductRepository.GetProductByIdAsync(id);
            if (product == null) return null;

            bool isInOrders = await _context.ProductRepository.IsProductInOrdersAsync(id);
            if (isInOrders)
            {
                product.IsActive = false;
                await _context.ProductRepository.UpdateProductAsync(product);
                await _context.SaveChangeAsync();
                return "desactivado";
            }

            if (product.Urls != null && product.Urls.Any())
            {
                foreach (var url in product.Urls)
                {
                    var publicId = CloudinaryHelper.ExtractPublicIdFromUrl(url);
                    if (!string.IsNullOrEmpty(publicId))
                        await _photoService.DeletePhotoAsync(publicId);
                }
            }

            await _context.ProductRepository.DeleteProductAsync(product);
            await _context.SaveChangeAsync();

            return "eliminado";
        }
    }
}