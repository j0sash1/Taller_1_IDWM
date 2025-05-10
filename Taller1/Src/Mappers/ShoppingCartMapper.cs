using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;

namespace Taller1.Src.Mappers
{
    public static class ShoppingCartMapper
    {
        public static ShoppingCartDto ToDto(ShoppingCart cart)
        {
            return new ShoppingCartDto
            {
                CartId = cart.CartId,
                Items = cart.Items.Select(item => new ShoppingItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price
                }).ToList()
            };
        }
    }
}