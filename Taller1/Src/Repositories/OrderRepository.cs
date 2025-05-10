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
    public class OrderRepository: IOrderRepository
    {
        private readonly StoreContext _context;
        public OrderRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<List<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}