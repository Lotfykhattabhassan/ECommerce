using MiniECommerce.Modules.Identity.Application.Abstractions.Persistence;
using MiniECommerce.Modules.Identity.Domain.Entities;
using MiniECommerce.Modules.Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MiniECommerce.Modules.Identity.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _context;

        public RefreshTokenRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken,
            CancellationToken cancellationToken)
        {
            await _context.RefreshTokens
                .AddAsync(refreshToken,
                cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenHashAsync(string hashToken,
            CancellationToken cancellationToken)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == hashToken,
                cancellationToken);

            return refreshToken;
        }
        public async Task RevokeTokenFamilyAsync(
            Guid tokenFamilyId,
            CancellationToken cancellationToken = default)
        {
            var refreshTokens = await _context.RefreshTokens
                .Where(x =>
                    x.TokenFamilyId == tokenFamilyId &&
                    x.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in refreshTokens)
            {
                token.Revoke();
            }
        }
        public async Task RevokeAllForUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var refreshTokens = await _context.RefreshTokens
                .Where(x =>
                    x.UserId == userId &&
                    x.RevokedAt == null)
                .ToListAsync(cancellationToken);

            foreach (var token in refreshTokens)
            {
                token.Revoke();
            }
        }
    }
}
