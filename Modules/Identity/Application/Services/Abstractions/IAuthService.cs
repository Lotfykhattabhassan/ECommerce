
using MiniECommerce.Modules.Identity.Application.Abstractions.Security;
using MiniECommerce.Modules.Identity.Application.DTOs.Auth;
using MiniECommerce.Modules.Identity.Application.DTOs.User;

namespace MiniECommerce.Modules.Identity.Application.Services.Abstractions
{
    public interface IAuthService
    {
        Task<Guid> RegisterUser(RegisterUserDto dto,
            CancellationToken cancellationToken = default);

        Task<JwtTokenResult> Login(LoginDto dto, CancellationToken cancellationToken = default);

        Task<ChangePasswordResponseDto> ChangeUserPassword(ChangePasswordDto dto, CancellationToken cancellationToken = default);

        Task<UserReadDto?> GetMe(CancellationToken cancellationToken = default);
    }
}
