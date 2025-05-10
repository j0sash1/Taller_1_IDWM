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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly UnitOfWork _unitOfWork;
        public ShoppingCartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ShoppingCartDto> GetCartAsync(string cartId)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetByCartIdAsync(cartId);
            return ShoppingCartMapper.ToDto(cart);
        }
        public async Task AddItemAsync(string cartId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor a 0", nameof(quantity));
            }
            var cart = await _unitOfWork.ShoppingCartRepository.GetByCartIdAsync(cartId);
            if (cart == null)
            {
                cart = new ShoppingCart { CartId = cartId };
                await _unitOfWork.ShoppingCartRepository.AddAsync(cart);
            }
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                throw new ArgumentException("Producto no encontrado", nameof(productId));
            }
            cart.AddItem(product, quantity);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task RemoveItemAsync(string cartId, int productId, int quantity)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetByCartIdAsync(cartId);
            if (cart == null)
            {
                throw new ArgumentException("Carrito no encontrado", nameof(cartId));
            }

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity -= quantity;
                if (item.Quantity <= 0)
                {
                    cart.RemoveItem(productId, quantity);  // Aquí pasamos los dos parámetros correctamente
                }
                await _unitOfWork.SaveChangeAsync();
            }
        }
        public async Task ClearCartAsync(string cartId)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetByCartIdAsync(cartId);
            if (cart == null)
            {
                throw new ArgumentException("Carrito no encontrado", nameof(cartId));
            }

            cart.ClearItems();
            await _unitOfWork.SaveChangeAsync();
        }
    }
}