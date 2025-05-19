using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Taller1.Src.Dtos.Order;
using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(CreateOrderDto dto, ClaimsPrincipal user);
    }
}