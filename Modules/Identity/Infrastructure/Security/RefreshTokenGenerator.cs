using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using System.Security.Cryptography;

namespace MiniECommerce.Modules.Identity.Infrastructure.Security
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string Generate()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", ""); ;
        }
    }
}
