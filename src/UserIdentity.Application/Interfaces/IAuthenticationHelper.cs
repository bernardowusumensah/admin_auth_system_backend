using System.IdentityModel.Tokens.Jwt;
using UserIdentity.Domain.Entities;

namespace UserIdentity.Application.Interfaces
{
    public interface IAuthenticationHelper
    {
        JwtSecurityToken GenerateJwtToken(Account account);
        string GenerateJwtTokenString(Account account);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
