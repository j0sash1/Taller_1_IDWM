using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;

namespace Taller1.Src.Mappers
{
    public class UserMapper
    {
        public static UserDto MapToDTO(User user) =>
             new()
             {
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 Thelephone = user.Thelephone,
                 Email = user.Email,
                 Street = user.ShippingAddres?.Street ?? string.Empty,
                 Number = user.ShippingAddres?.Number ?? string.Empty,
                 Commune = user.ShippingAddres?.Commune ?? string.Empty,
                 Region = user.ShippingAddres?.Region ?? string.Empty,
                 PostalCode = user.ShippingAddres?.PostalCode ?? string.Empty,
             };
    }
}