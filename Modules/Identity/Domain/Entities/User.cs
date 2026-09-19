using MiniECommerce.BuildingBlocks.Domain.Common;
using MiniECommerce.Modules.Identity.Domain.Enums;

namespace MiniECommerce.Modules.Identity.Domain.Entities
{
    public class User : Entity<Guid>
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public UserCredential Credential { get; private set; }
        public UserStatus Status { get; private set; }

        private readonly List<UserRole> _roles = new();
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();
        private User(Guid id,
            string firstName,
            string lastName,
            string email
            )
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException(nameof(firstName));
            FirstName = firstName;

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException(nameof(lastName));
            LastName = lastName;

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));
            Email = email;

            Status = UserStatus.Active;
        }
        protected User()
        {
            
        }

        public static User Create(
            string firstName,
            string lastName,
            string email)
        {

            return new User(Guid.NewGuid(), firstName, lastName, email);
        }

        public void ChangeUserName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException(nameof(firstName));
            FirstName = firstName;

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException(nameof(lastName));
            LastName = lastName;
        }

        public void DisableUser()
        {
            if (Status == UserStatus.Inactive)
                return;

            Status = UserStatus.Inactive;
        }
    }
}
