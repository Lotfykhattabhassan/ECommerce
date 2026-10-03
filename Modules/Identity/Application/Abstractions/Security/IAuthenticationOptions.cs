
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public interface IAuthenticationOptions
    {
        int RefreshTokenLifetimeDays { get; }
    }
}
