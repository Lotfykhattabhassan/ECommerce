using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Identity.Domain.Entities
{
    public class Role : Entity<int>
    {
        public string RoleName { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Role(string roleName,
            string description)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException(nameof(roleName));

            RoleName = roleName;

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(nameof(description));

            Description = description;
        }
        public static Role Create(string roleName,
            string description)
        {
            return new Role(roleName, description);
        }

        public void ChangeRoleName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException(nameof(newName));

            RoleName = newName;
            MarkAsUpdated();
        }
        public void ChangeRoleDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
                throw new ArgumentException(nameof(newDescription));

            Description = newDescription;
            MarkAsUpdated();
        }
    }
}
