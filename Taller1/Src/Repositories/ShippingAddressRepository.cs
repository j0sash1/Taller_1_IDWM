using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Data;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;

using Microsoft.EntityFrameworkCore;

namespace Taller1.Src.Repositories
{
    public class ShippingAddressRepository(StoreContext context) : IShippingAddressRepository
    {
        private readonly StoreContext _context = context;

        public async Task<ShippingAddres?> GetByUserIdAsync(string userId)
        {
            return await _context.ShippingAddres
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task AddAsync(ShippingAddres address)
        {
            await _context.ShippingAddres.AddAsync(address);
        }
    }
}