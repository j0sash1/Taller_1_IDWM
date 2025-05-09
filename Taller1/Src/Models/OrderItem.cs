using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}