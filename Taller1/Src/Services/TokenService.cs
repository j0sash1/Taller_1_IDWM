using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Taller1.Src.Interfaces;
using Taller1.Src.Models;

using Microsoft.IdentityModel.Tokens;

namespace Taller1.Src.Services
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            var signingKey = _config["Jwt:SignInKey"] ?? throw new ArgumentNullException("Key not found");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

        }
        public string GenerateToken(User user, string role)
        {
            var claims = new List<Claim>
            {
               new(JwtRegisteredClaimNames.Email, user.Email!),
               new(JwtRegisteredClaimNames.GivenName, user.FirstName),
               new(ClaimTypes.Role, role),
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
