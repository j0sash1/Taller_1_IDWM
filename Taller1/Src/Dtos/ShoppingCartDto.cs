using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;

namespace Taller1.Src.Dtos
{
    public class ShoppingCartDto
    {
        public string CartId { get; set; } = string.Empty;
        public List<ShoppingItemDTo> Items { get; set; } = new();
        public decimal Total => Items.Sum(item => item.Price * item.Quantity);
    }
}