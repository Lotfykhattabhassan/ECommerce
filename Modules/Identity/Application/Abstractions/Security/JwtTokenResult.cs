
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public record JwtTokenResult(string accessToken,DateTime expiresAt)
    {
    }
}
