using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class OrderSummaryDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Total { get; set; }
    }
}