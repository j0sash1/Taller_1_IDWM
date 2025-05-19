using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;

namespace Taller1.Src.Mappers
{
    public static class OrderMapper
    {
        public static Order FromShoppingCart(ShoppingCart shoppingCart, string userId, int shippingAddressId)
        {
            return new Order
            {
                UserId = userId,
                ShippingAddressId = shippingAddressId,
                Total = shoppingCart.Items.Sum(i => i.Quantity * i.Product.Price),
                Items = shoppingCart.Items.Select(i => new OrderItem
                {
                    ProductId = i.Product.Id,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.Product.Price
                }).ToList()
            };
        }

        public static OrderDto ToOrderDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.OrderDate,
                Address = order.ShippingAddress,
                Total = (int)Math.Floor(order.Total),
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    Name = i.ProductName,
                    Quantity = i.Quantity,
                    Price = (int)Math.Floor(i.Price),
                    ImageUrl = "" // Puedes ajustar si decides guardar o mapear imágenes
                }).ToList()
            };
        }

        public static OrderSummaryDto ToSummaryDto(Order order)
        {
            return new OrderSummaryDto
            {
                Id = order.Id,
                CreatedAt = order.OrderDate,
                Total = (int)Math.Floor(order.Total)
            };
        }
    }
}