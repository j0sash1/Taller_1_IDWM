using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Creado";
        public decimal Total { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}