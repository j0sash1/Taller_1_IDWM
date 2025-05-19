using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart?> GetShoppingCartAsync(string cartId);
        ShoppingCart CreateShoppingCart(string cartId);
        void UpdateShoppingCart(ShoppingCart shoppingCart);
        void DeleteShoppingCart(ShoppingCart shoppingCart);
    }
}