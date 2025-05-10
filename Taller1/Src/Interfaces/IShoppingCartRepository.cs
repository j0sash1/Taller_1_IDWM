using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Interfaces
{
    public class IShoppingCartRepository
    {
        Task<ShoppingCart> GetByCartIdAsync(string cartId);  
        Task AddAsync(ShoppingCart cart);  
        Task UpdateAsync(ShoppingCart cart);  
        Task RemoveAsync(string cartId);  
    }
}