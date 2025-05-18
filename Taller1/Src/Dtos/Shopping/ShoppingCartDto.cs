using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;

namespace Taller1.Src.Dtos
{
    public class ShoppingCartDto
    {
        public required string CartId { get; set; }        
         public List<ShoppingItemDTo> Items { get; set; } = [];
    }
}