using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string firstName);
        Task CreatedUserAsync(User user, ShippingAddres? shippingAddres);
        void UpdateUserAsync(User user);
        void UpdateShippingAddressAsync(UserDto userDto);
        void DeleteUserAsync(User user, ShippingAddres shippingAddres);

    }
}