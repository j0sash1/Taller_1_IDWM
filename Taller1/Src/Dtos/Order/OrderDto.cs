using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ShippingAddres Address { get; set; } = null!;
        public int Total { get; set; } // En CLP, sin decimales
        public List<OrderItemDto> Items { get; set; } = [];
    }
}