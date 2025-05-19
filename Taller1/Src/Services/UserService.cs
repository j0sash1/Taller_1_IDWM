/**
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Taller1.Src.Data;
using Taller1.Src.Dtos;
using Taller1.Src.Interfaces;
using Taller1.Src.Mappers;
using Taller1.Src.Models;
  
namespace Taller1.Src.Services
{
    public class UserService(UnitOfWork unitOfWork) : IUserService
    {
        private readonly UnitOfWork _unitOfWorkork = unitOfWork;

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWorkork.UserManager.Users.ToListAsync();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
        {
            if (userDto.ConfirmPassword != userDto.Password)
            {
                throw new Exception("Passwords do not match");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Telephone = userDto.telephone,
                ShippingAddres = new ShippingAddres
                {
                    Street = userDto.Street ?? string.Empty,
                    Number = userDto.Number ?? string.Empty,
                    Commune = userDto.Commune ?? string.Empty,
                    Region = userDto.Region ?? string.Empty,
                    PostalCode = userDto.PostalCode ?? string.Empty,
                },

                UserName = userDto.Email,
                NormalizedUserName = userDto.Email.ToUpper(),
                Email = userDto.Email,
                NormalizedEmail = userDto.Email.ToUpper(),
            };

            IdentityResult result = await _unitOfWorkork.UserManager.CreateAsync(
                user,
                userDto.Password
            );

            if (!result.Succeeded)
            {
                throw new Exception(
                    "User creation failed: "
                        + string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            await _unitOfWorkork.SaveChangeAsync();

            UserDto dto = UserMapper.UserToUserDto(user);

            return dto;
        }
    }
}
**/