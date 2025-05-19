using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;

namespace Taller1.Src.Dtos.Shopping
{
    public class ShoppingCartDto
    {
        public required string ShoppingCartId { get; set; }
        public List<ShoppingItemDTo> Items { get; set; } = [];
    }
}