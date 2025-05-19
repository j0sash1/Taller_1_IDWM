using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Repositories
{
    public class ShoppingCartRepository(StoreContext context) : IShoppingCartRepository
    {
        private readonly StoreContext _context = context;

        public async Task<ShoppingCart?> GetShoppingCartAsync(string cartId)
        {
            return await _context.ShoppingCarts
                .Include(x => x.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.CartId == cartId);
        }

        public ShoppingCart CreateShoppingCart(string cartId)
        {
            var shoppingCart = new ShoppingCart { CartId = cartId };
            _context.ShoppingCarts.Add(shoppingCart);
            return shoppingCart;
        }

        public void UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Update(shoppingCart);
        }

        public void DeleteShoppingCart(ShoppingCart shoppingCart)
        {
            _context.ShoppingCarts.Remove(shoppingCart);
        }
    }
}