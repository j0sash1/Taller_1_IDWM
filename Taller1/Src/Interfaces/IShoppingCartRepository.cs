using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;
namespace Taller1.Src.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetByCartIdAsync(string cartId);
        Task AddAsync(ShoppingCart cart);
        Task UpdateAsync(ShoppingCart cart);
        Task RemoveAsync(string cartId);
    }
}