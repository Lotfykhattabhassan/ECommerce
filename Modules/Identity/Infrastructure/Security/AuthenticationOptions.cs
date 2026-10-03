using MiniECommerce.Modules.Identity.Application.Abstractions.Security;

namespace MiniECommerce.Modules.Identity.Infrastructure.Security;

public class AuthenticationOptions : IAuthenticationOptions
{
    public int RefreshTokenLifetimeDays { get; set; }
}