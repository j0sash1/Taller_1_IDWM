using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data;
using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Repositories
{
    public class UserRepository(UserManager<User> userManager, StoreContext context) : IUserRepository

    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly StoreContext _context = context;

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userManager.Users
                .Include(u => u.ShippingAddres)
                .ToListAsync();
        }
        public async Task CreatedUserAsync(User user, string password, ShippingAddres shippingAddres)
        {
            await _userManager.CreateAsync(user, password);
            _context.ShippingAddresses.Add(shippingAddres);
            await _context.SaveChangesAsync();
        }

    }
}