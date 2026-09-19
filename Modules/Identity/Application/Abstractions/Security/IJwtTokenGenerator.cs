
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public interface IJwtTokenGenerator
    {
        JwtTokenResult Generate(Guid userId, string email, IReadOnlyCollection<string> roles);
    }
}
