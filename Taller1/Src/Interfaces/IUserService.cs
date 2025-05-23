using Taller1.Src.Dtos.User;
using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
    }
}