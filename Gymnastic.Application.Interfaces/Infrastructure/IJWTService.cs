using Gymnastic.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Gymnastic.Infrastructure.Authentication
{
    public interface IJWTService
    {
        JwtSecurityToken GenerateToken(ApplicationUser user, IList<string> roles, IList<Claim> userClaims);
    }
}
