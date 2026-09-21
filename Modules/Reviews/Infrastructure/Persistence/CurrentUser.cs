using Microsoft.AspNetCore.Http;
using MiniECommerce.Modules.Reviews.Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniECommerce.Modules.Reviews.Infrastructure.Persistence
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId { get {
                var value = _httpContextAccessor
                        .HttpContext?
                        .User
                        .FindFirst(JwtRegisteredClaimNames.Sub)?
                        .Value;
                if (!Guid.TryParse(value, out Guid result))
                    return null;
                return result;
            } }

        public IReadOnlyList<string> Roles
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindAll(ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToList()
                    ?? [];
            }
        }

    }
}
