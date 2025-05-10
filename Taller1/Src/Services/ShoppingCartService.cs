using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Services
{
    public class ShoppingCartService: IShoppingCartService
    {
        private readonly UnitOfwork _unitOfWork;
        public ShoppingCartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork
        }
        public async Task<ShoppingCartDto> GetCartAsync(string cartId)
        {
            var cart = await _unitOfWork.ShoppingCartRepository.GetByCartIdAsync(cartId);
            return ShoppingCartMapper.ToDto(cart);
        }
        public async Task AddItemAsync(string cartId, int productId, int quantity)
        {
        // Implementar lógica para añadir producto al carrito
        }
        public async Task RemoveItemAsync(string cartId, int productId, int quantity)
        {
        // Implementar lógica para remover producto del carrito
        }
        public async Task ClearCartAsync(string cartId)
        {
        // Implementar lógica para vaciar carrito
        }
    }
}