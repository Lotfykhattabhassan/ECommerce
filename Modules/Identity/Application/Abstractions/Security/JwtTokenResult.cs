
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public record JwtTokenResult(
    string accessToken,
    string refreshToken,
    DateTime accessTokenExpiresAt,
    DateTime refreshTokenExpiresAt);
}
