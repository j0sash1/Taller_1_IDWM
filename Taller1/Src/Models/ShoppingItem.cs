using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Models
{
    [Table("ShoppingItems")]
    public class ShoppingItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public required Product Product { get; set; }

        public int CartId { get; set; }

        public ShoppingCart shoppingCart { get; set; } = null!;
    }
}