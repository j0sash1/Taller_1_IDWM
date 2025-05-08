using Microsoft.AspNetCore.Identity;

using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class UnitOfWork(
        StoreContext context,
        IProductRepository productRepository,
        IUserRepository userRepository,
        UserManager<User> userManager
    )
    {
        private readonly StoreContext _context = context;

        // public IUserRepository UserRepository { get; set; } = userRepository;
        public UserManager<User> UserManager = userManager;

        public IProductRepository ProductRepository { get; set; } = productRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}