using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;

namespace Taller1.Src.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, IUserRepository userRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
        }
        public async Task<Order> CreateOrderAsync(CreateOrderDto dto, ClaimsPrincipal userPrincipal)
        {
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) throw new Exception("Usuario no identificado");

            var orderItems = new List<OrderItem>();
            decimal total = 0;
            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepo.GetProductByIdAsync(itemDto.ProductId);
                if (product == null) throw new Exception($"Producto ID {itemDto.ProductId} no encontrado");

                var orderItem = new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = itemDto.Quantity
                };
                orderItems.Add(orderItem);
                total += product.Price * itemDto.Quantity;
            }
            var order = new Order
            {
                UserId = userId,
                Address = dto.Address,
                OrderDate = DateTime.UtcNow,
                Items = orderItems,
                Total = total
            };

            await _orderRepo.AddAsync(order);
            return order;
        }
    }
}