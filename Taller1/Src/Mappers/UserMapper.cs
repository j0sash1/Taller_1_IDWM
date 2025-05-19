using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Taller1.Src.Dtos;
using Taller1.Src.Dtos.Auth;
using Taller1.Src.Dtos.User;
using Taller1.Src.Models;

namespace Taller1.Src.Mappers
{
    public class UserMapper
    {
        public static User RegisterToUser(RegisterDto dto) =>
            new()
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.Telephone,
                Telephone = dto.Telephone,
                RegisteredAt = DateTime.UtcNow,
                IsActive = true,
                ShippingAddres = new ShippingAddres
                {
                    Street = dto.Street ?? string.Empty,
                    Number = dto.Number ?? string.Empty,
                    Commune = dto.Commune ?? string.Empty,
                    Region = dto.Region ?? string.Empty,
                    PostalCode = dto.PostalCode ?? string.Empty
                }
            };
        public static UserDto UserToUserDto(User user) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                Telephone = user.PhoneNumber ?? string.Empty,
                Street = user.ShippingAddres?.Street,
                Number = user.ShippingAddres?.Number,
                Commune = user.ShippingAddres?.Commune,
                Region = user.ShippingAddres?.Region,
                PostalCode = user.ShippingAddres?.PostalCode,
                RegisteredAt = user.RegisteredAt,
                LastAccess = user.LastAccess,
                IsActive = user.IsActive
            };
        public static AuthenticatedUserDto UserToAuthenticatedDto(User user, string token) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,

                Token = token,


            };
        public static NewUserDto UserToNewUserDto(User user) =>
            new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty
            };
        public static void UpdateUserFromDto(User user, UpdateProfileDto dto)
        {
            if (dto.FirstName is not null)
                user.FirstName = dto.FirstName;

            if (dto.LastName is not null)
                user.LastName = dto.LastName;

            if (dto.Email is not null)
                user.Email = dto.Email;

            if (dto.Phone is not null)
                user.Telephone = dto.Phone;

            if (dto.BirthDate.HasValue)
                user.BirthDate = dto.BirthDate.Value;
        }
    }
}