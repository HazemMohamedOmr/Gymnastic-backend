using Gymnastic.Application.Interface.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;


namespace Gymnastic.Infrastructure.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
        public string Email => _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
        public IEnumerable<string> Roles => _httpContextAccessor.HttpContext?.User?.FindAll("roles")?.Select(c => c.Value) ?? Enumerable.Empty<string>();
        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
