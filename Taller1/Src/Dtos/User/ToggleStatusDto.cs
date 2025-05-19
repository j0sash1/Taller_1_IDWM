using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.Dtos.User
{
    public class ToggleStatusDto
    {
        [StringLength(255)]
        public string? Reason { get; set; }
    }
}