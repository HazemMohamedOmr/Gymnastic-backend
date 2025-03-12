using Gymnastic.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Gymnastic.Application.Interface.Services
{
    public interface IJWTTokenService
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
