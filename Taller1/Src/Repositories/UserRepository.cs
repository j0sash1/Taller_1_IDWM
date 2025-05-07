using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Repositories
{
    // public class UserRepository(StoreContext store) : IUserRepository
    // {
    //     private readonly UserManager<User> _userManager = userManager;
    //     public IQueryable<User> GetUsersQueryable()
    //     {
    //         return _userManager.Users.Include(u => u.ShippingAddres).AsQueryable();
    //     }

    //     public async Task<User?> GetUserByIdAsync(string id)
    //     {
    //         return await _userManager.Users
    //             .Include(u => u.ShippingAddres)
    //             .FirstOrDefaultAsync(u => u.Id == id);
    //     }

    //     public async Task<User?> GetUserByEmailAsync(string email)
    //     {
    //         return await _userManager.Users
    //             .Include(u => u.ShippingAddres)
    //             .FirstOrDefaultAsync(u => u.Email == email);
    //     }

    //     public async Task UpdateUserAsync(User user)
    //     {
    //         await _userManager.UpdateAsync(user);
    //     }
    //     public async Task<User?> GetByEmailAsync(string email)
    //     {
    //         return await _userManager.Users
    //             .Include(u => u.ShippingAddres)
    //             .FirstOrDefaultAsync(u => u.Email == email);
    //     }

    //     public async Task<bool> CheckPasswordAsync(User user, string password)
    //     {
    //         var hasher = new PasswordHasher<User>();
    //         var result = hasher.VerifyHashedPassword(user, user.PasswordHash!, password);
    //         return result == PasswordVerificationResult.Success;
    //     }
    // }
}