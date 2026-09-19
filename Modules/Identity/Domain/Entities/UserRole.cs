using MiniECommerce.BuildingBlocks.Domain.Common;
using System.Data;

namespace MiniECommerce.Modules.Identity.Domain.Entities
{
    public class UserRole : Entity<int>
    {
        public Guid UserId { get; private set; }
        public int RoleId { get; private set; }
        public User User { get; private set; } = null!;
        public Role Role { get; private set; } = null!;
        public UserRole(Guid userId, int roleId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));
            UserId = userId;

            if (roleId <= 0)
                throw new ArgumentException(nameof(roleId));

            RoleId = roleId;
        }

        public static UserRole Create(Guid userId, int roleId)
        {
            return new UserRole(userId, roleId);
        }

        public void ChangeUserRole(int roleId)
        {
            if (roleId <= 0)
                throw new ArgumentException(nameof(roleId));

            RoleId = roleId;
        }
    }
}
