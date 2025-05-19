using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;
using Taller1.Src.Dtos.Shopping;

namespace Taller1.Src.Mappers
{
    public static class ShoppingCartMapper
    {
        public static ShoppingCartDto ToDto(this ShoppingCart shoppingCart)
        {
            return new ShoppingCartDto
            {
                ShoppingCartId = shoppingCart.CartId,
                Items = [.. shoppingCart.Items.Select(x => new ShoppingItemDTo
                {
                    ProductId = x.ProductId,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    PictureUrl = x.Product.Urls?.FirstOrDefault() ?? string.Empty,
                    Brand = x.Product.Brand,
                    Category = x.Product.Category,
                    Quantity = x.Quantity
                })]
            };
        }
    }
}