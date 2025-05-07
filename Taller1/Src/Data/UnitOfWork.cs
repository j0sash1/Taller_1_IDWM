using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Interfaces;

namespace Taller1.Src.Data
{
    public class UnitOfWork(StoreContext context, IProductRepository productRepository, UserManager<User> userManager)
    {
        private readonly StoreContext _context = context;
        // public IUserRepository UserRepository { get; set; } = userRepository;
        public UserManager<User> UserManager = userManager;

        public IProductRepository ProductRepository { get; set; } = productRepository;

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}