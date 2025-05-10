using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Mappers
{
    public static class ShoppingCartMapper
    {
        return new ShoppingCartDto
        {
            CartId = ShoppingCartMapper.CartId,
            Items = ShoppingCartMapper.Items.Select(item => new ShoppingItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                Price = item.Product.Price
    }).ToList()
        };
    }
}