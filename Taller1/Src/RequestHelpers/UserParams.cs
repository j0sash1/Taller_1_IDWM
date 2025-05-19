using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.RequestHelpers
{
    public class UserParams : PaginationParams
    {
        public bool? IsActive { get; set; }
        public DateTime? RegisteredFrom { get; set; }
        public DateTime? RegisteredTo { get; set; }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; } = "dateDesc";
    }
}