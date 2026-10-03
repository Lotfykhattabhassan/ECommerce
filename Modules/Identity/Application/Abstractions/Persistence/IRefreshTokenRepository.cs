using MiniECommerce.Modules.Identity.Domain.Entities;

namespace MiniECommerce.Modules.Identity.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenHashAsync(
            string hashToken,
            CancellationToken cancellationToken = default);
        Task RevokeTokenFamilyAsync(
            Guid tokenFamilyId,
            CancellationToken cancellationToken = default);
        Task AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken = default);
        Task RevokeAllForUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default);
    }
}
