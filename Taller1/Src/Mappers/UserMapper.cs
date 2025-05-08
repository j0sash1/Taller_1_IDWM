using Taller1.Src.Dtos;
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
                PhoneNumber = dto.Thelephone,
                Thelephone = dto.Thelephone,
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
                Thelephone = user.PhoneNumber ?? string.Empty,
                Street = user.ShippingAddres?.Street,
                Number = user.ShippingAddres?.Number,
                Commune = user.ShippingAddres?.Commune,
                Region = user.ShippingAddres?.Region,
                PostalCode = user.ShippingAddres?.PostalCode,
                RegisteredAt = user.RegisteredAt,
                LastAccess = user.LastAccess,
                IsActive = user.IsActive
            };
    }
}