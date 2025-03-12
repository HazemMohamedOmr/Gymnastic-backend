using Gymnastic.Application.Interface.Services;
using Gymnastic.Domain.Models;
using Gymnastic.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Gymnastic.Application.UseCases.Commons.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTService _jwtService;

        public JWTTokenService(UserManager<ApplicationUser> userManager, IJWTService jwtService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return _jwtService.GenerateToken(user, roles, userClaims);
        }
    }
}
