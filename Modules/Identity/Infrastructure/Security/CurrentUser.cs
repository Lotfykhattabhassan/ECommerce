using Microsoft.AspNetCore.Http;
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using System.IdentityModel.Tokens.Jwt;

namespace MiniECommerce.Modules.Identity.Infrastructure.Security
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst(JwtRegisteredClaimNames.Sub)?
                    .Value;

                if (!Guid.TryParse(value, out var result))
                    return null;

                return result;
            }
        }

        public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?
            .User
            .Identity?
            .IsAuthenticated == true;
    }
}