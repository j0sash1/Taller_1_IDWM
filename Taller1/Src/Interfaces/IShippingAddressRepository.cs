using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IShippingAddressRepository
    {
        Task<ShippingAddres?> GetByUserIdAsync(string userId);
        Task AddAsync(ShippingAddres address);
    }
}