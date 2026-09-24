using Microsoft.AspNetCore.Http;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;

namespace MiniECommerce.Modules.Cart.Infrastructure.Persistence
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? UserId { get
            {
                var result = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst(JwtRegisteredClaimNames.Sub)?
                    .Value;

                if (!Guid.TryParse(result, out Guid value))
                    return null;
                return value;
            } }
    }
}
