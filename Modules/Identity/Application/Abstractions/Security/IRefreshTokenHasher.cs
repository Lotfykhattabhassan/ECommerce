
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public interface IRefreshTokenHasher
    {
        string Hash(string refreshToken);
    }
}
