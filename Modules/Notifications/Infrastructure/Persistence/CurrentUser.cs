using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using System.Security.Claims;

namespace MiniECommerce.Modules.Notifications.Infrastructure.Persistence
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId { get {
                var result = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst(JwtRegisteredClaimNames.Sub)?
                        .Value;
                if (!Guid.TryParse(result, out Guid value))
                    return null;

                return value;
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
