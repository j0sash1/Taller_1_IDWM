using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taller1.Src.RequestHelpers
{
    public class PaginationParams
    {
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;       // <-- requerido por ToPagedList
        private int _pageSize = 20;

        public int PageSize                           // <-- requerido por ToPagedList
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}