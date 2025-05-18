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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly StoreContext _context;
        public ShoppingCartRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<ShoppingCart> GetByCartIdAsync(string cartId)
        {
            return await _context.ShoppingCarts
                .Include(cart => cart.Items)
                .FirstOrDefaultAsync(cart => cart.CartId == cartId);
        }
        public async Task AddAsync(ShoppingCart cart)
        {
            await _context.ShoppingCarts.AddAsync(cart);
        }
        public async Task UpdateAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
        }
        public async Task RemoveAsync(string cartId)
        {
            var cart = await _context.ShoppingCarts.FirstOrDefaultAsync(c => c.CartId == cartId);
            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
            }
        }
    }
}