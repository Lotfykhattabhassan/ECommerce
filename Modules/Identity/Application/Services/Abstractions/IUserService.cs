
using MiniECommerce.Modules.Identity.Application.DTOs.User;
using MiniECommerce.Modules.Identity.Application.DTOs.UserRoleDto;

namespace MiniECommerce.Modules.Identity.Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken = default);

        Task<UserReadDto?> GetUserById(Guid id, CancellationToken cancellationToken = default);
        Task<DeleteUserResponseDto> DeleteUser(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UserReadDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UserRoleReadDto>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default);
        Task<UpdateUserResponseDto> UpdateUserName(UpdateUserDto dto, CancellationToken cancellationToken = default);
    }
}
