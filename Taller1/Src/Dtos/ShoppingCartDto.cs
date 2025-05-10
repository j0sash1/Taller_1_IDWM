using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class ShoppingCartDto
    {
        public string CartId { get; set; } = string.Empty;
        public List<ShoppingItemDto> Items { get; set; } = new();
        public decimal Total => Items.Sum(in => int.Price * int.Quantity); 
    }
}