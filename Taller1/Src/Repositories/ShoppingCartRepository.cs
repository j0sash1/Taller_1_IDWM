using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;
using Taller1.Src.Data;
using Taller1.Src.Interfaces;

namespace Taller1.Src.Repositories
{
    public interface ShoppingCartRepository
    {
        Task<ShoppingCart> GetByCartIdAsync(string cartId);
        Task AddAsync(ShoppingCart cart);
        Task UpdateAsync(ShoppingCart cart);
        Task DeleteAsync(ShoppingCart cart);
    }
}