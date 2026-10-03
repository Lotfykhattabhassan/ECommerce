using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Identity.Domain.Entities
{
    public class RefreshToken : Entity<Guid>
    {
        public Guid UserId { get; private set; }

        public string TokenHash { get; private set; }
        public Guid TokenFamilyId { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public byte[] Version { get; private set; } = null!;
        public DateTime? RevokedAt { get; private set; }

        public Guid? ReplacedByTokenId { get; private set; }
        private RefreshToken() { }
        private RefreshToken(Guid id,
            Guid userId,
            string tokenHash,
            Guid tokenFamilyId,
            DateTime expiresAt)
            : base(id)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));
            UserId = userId;

            if (string.IsNullOrWhiteSpace(tokenHash))
                throw new ArgumentException(nameof(tokenHash));
            TokenHash = tokenHash;

            if (tokenFamilyId == Guid.Empty)
                throw new ArgumentException(nameof(tokenFamilyId));
            TokenFamilyId = tokenFamilyId;

            if (DateTime.UtcNow >= expiresAt)
                throw new ArgumentException(nameof(expiresAt));
            ExpiresAt = expiresAt;

            CreatedAt = DateTime.UtcNow;
        }
        public static RefreshToken Create(Guid userId,
            string tokenHash,
            Guid tokenFamilyId,
            DateTime expiresAt)
        {
            return new RefreshToken(Guid.NewGuid(), userId, tokenHash, tokenFamilyId, expiresAt);
        }
        public bool IsExpired =>
            DateTime.UtcNow >= ExpiresAt;

        public bool IsRevoked =>
            RevokedAt.HasValue;

        public bool IsActive =>
            !IsExpired && !IsRevoked;

        public void Revoke(Guid? replacedByTokenId = null)
        {
            RevokedAt = DateTime.UtcNow;
            ReplacedByTokenId = replacedByTokenId;
        }
    }
}
