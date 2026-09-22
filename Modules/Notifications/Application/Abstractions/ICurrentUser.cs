namespace MiniECommerce.Modules.Notifications.Application.Abstractions
{
    public class ICurrentUser
    {
        public Guid? UserId { get; }
        public IReadOnlyList<string> Roles { get; }
    }
}
