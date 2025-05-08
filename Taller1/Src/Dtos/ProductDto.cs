using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos
{
    public class ProductDto
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no debe superar los 1000 caracteres.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public required string Category { get; set; }

        [Required(ErrorMessage = "La marca del producto es obligatoria.")]
        public required string Brand { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser igual o mayor a 0.")]
        public int Stock { get; set; }

        public string[]? Urls { get; set; }
    }
}