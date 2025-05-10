using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}