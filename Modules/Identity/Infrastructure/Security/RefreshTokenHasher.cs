using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using System.Security.Cryptography;
using System.Text;

namespace MiniECommerce.Modules.Identity.Infrastructure.Security
{
    public class RefreshTokenHasher : IRefreshTokenHasher
    {
        public string Hash(string refreshToken)
        {
            var bytes = SHA256.HashData(
            Encoding.UTF8.GetBytes(refreshToken));

            return Convert.ToHexString(bytes);
        }
    }
}
