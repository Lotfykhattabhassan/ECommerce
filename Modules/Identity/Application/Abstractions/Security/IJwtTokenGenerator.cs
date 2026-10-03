
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public interface IJwtTokenGenerator
    {
        JwtAccessTokenResult Generate(Guid userId, string email, IReadOnlyCollection<string> roles);
    }
}
