
using MiniECommerce.Modules.Identity.Application.DTOs.Role;

namespace MiniECommerce.Modules.Identity.Application.Services.Abstractions
{
    public interface IRoleService
    {
        Task<int> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken = default);
        Task<RoleReadDto?> GetRoleById(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RoleReadDto>> GetAllRoles(CancellationToken cancellationToken = default);
        Task<UpdateRoleResponseDto> UpdateRoleAsync(UpdateRoleDto dto, CancellationToken cancellationToken = default);
        Task<DeleteRoleResponseDto> DeleteRoleAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
