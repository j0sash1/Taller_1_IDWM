using Microsoft.AspNetCore.Identity;

using Taller1.Src.Interfaces;
using Taller1.Src.Models;

namespace Taller1.Src.Data
{
    public class UnitOfWork(
        StoreContext context,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IShoppingCartRepository shoppingCartRepository,
        UserManager<User> userManager
    )
    {
        private readonly StoreContext _context = context;

        private readonly StoreContext _context = context;
        public IOrderRepository OrderRepository { get; set; } = orderRepository;
        public IProductRepository ProductRepository { get; set; } = productRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = shoppingCartRepository;
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}