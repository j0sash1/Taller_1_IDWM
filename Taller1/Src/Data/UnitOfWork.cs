using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        IOrderRepository orderRepository,
        IShippingAddressRepository ShippingAddressRepository
    )
    {
        private readonly StoreContext _context = context;

        public IOrderRepository OrderRepository { get; set; } = orderRepository;
        public IProductRepository ProductRepository { get; set; } = productRepository;
        public IUserRepository UserRepository { get; set; } = userRepository;
        public IShoppingCartRepository ShoppingCartRepository { get; set; } = shoppingCartRepository;
        public IShippingAddressRepository ShippingAddressRepository { get; set; } = ShippingAddressRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}