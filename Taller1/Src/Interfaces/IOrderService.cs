using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Taller1.Src.Models;
using Taller1.Src.Dtos;

namespace Taller1.Src.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto dto, ClaimsPrincipal user);
    }
}