using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartDto> GetCartAsync(string cartId);
        Task AddItemAsync(string cartId, int productId, int quantity);
        Task RemoveItemAsync(string cartId, int productId, int quantity);
        Task ClearCartAsync(string cartId);
    }
}