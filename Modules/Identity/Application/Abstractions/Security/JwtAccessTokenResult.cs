
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public record JwtAccessTokenResult(string accessToken,
        DateTime accessTokenExpiresAt);
    
}
