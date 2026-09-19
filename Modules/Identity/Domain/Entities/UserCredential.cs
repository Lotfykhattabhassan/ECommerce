using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Identity.Domain.Enums;

namespace MiniECommerce.Modules.Identity.Domain.Entities
{
    public class UserCredential : Entity<int>
    {
        public Guid UserId { get; private set; }
        public string PasswordHash { get; private set; } = null!;
        public DateTime PasswordChangedAt { get; private set; }

        public int FailedLoginAttempts { get; private set; }
        public DateTime? LockedUntil { get; private set; }
        public CredentialStatus Status { get; private set; }
        public User User { get; private set; }

        private const int MaxFailedLoginAttempts = 4;

        private static readonly TimeSpan LockoutDuration =
            TimeSpan.FromMinutes(15);

        private UserCredential()
        {
        }

        private UserCredential(Guid userId, string passwordHash)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(
                    "userId cannot be empty",
                    nameof(userId));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException(
                    "Password hash cannot be empty.",
                    nameof(passwordHash));

            UserId = userId;
            PasswordHash = passwordHash;
            Status = CredentialStatus.Active;
            FailedLoginAttempts = 0;
            PasswordChangedAt = DateTime.UtcNow;
        }

        public static UserCredential Create(
            Guid userId,
            string passwordHash)
        {
            return new UserCredential(
                userId,
                passwordHash);
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (Status == CredentialStatus.Disabled)
                throw new InvalidOperationException(
                    "Disabled credentials cannot change password.");

            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException(
                    "New password hash cannot be empty.",
                    nameof(newPasswordHash));

            PasswordHash = newPasswordHash;
            PasswordChangedAt = DateTime.UtcNow;

            FailedLoginAttempts = 0;
            LockedUntil = null;
            Status = CredentialStatus.Active;

            MarkAsUpdated();
        }

        public void CanLogin()
        {
            if (Status == CredentialStatus.Disabled)
                throw new InvalidOperationException(
                    "Disabled credentials cannot login.");

            if (LockedUntil.HasValue)
            {
                if (LockedUntil.Value > DateTime.UtcNow)
                {
                    throw new InvalidOperationException(
                        $"Account is locked until {LockedUntil.Value}.");
                }

                // Lockout expired
                LockedUntil = null;
                Status = CredentialStatus.Active;
                FailedLoginAttempts = 0;

                MarkAsUpdated();
            }
        }

        public void SuccessfulLogin()
        {
            FailedLoginAttempts = 0;
            MarkAsUpdated();
        }

        public void FailedLogin()
        {
            if (Status == CredentialStatus.Disabled)
                throw new InvalidOperationException(
                    "Disabled credentials cannot login.");

            if (LockedUntil.HasValue && LockedUntil.Value > DateTime.UtcNow)
                throw new InvalidOperationException(
                    $"Account is locked until {LockedUntil.Value}.");

            FailedLoginAttempts++;

            if (FailedLoginAttempts >= MaxFailedLoginAttempts)
            {
                Lock(DateTime.UtcNow.Add(LockoutDuration));
                return;
            }

            MarkAsUpdated();
        }

        public void Lock(DateTime until)
        {
            if (until <= DateTime.UtcNow)
                throw new ArgumentException(
                    "Lock until time must be in the future.",
                    nameof(until));

            LockedUntil = until;
            Status = CredentialStatus.Locked;

            MarkAsUpdated();
        }

        public void Unlock()
        {
            LockedUntil = null;
            Status = CredentialStatus.Active;
            FailedLoginAttempts = 0;

            MarkAsUpdated();
        }

        public void Disable()
        {
            Status = CredentialStatus.Disabled;

            MarkAsUpdated();
        }
    }
}
