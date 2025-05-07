using Taller1.Src.Models;

namespace Taller1.Src.Interfaces
{
    public interface ITokenServices
    {
        string GenerateToken(User user, string role);
    }
}