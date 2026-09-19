
namespace MiniECommerce.Modules.Identity.Application.Abstractions.Security
{
    public interface ICurrentUser
    {
        public Guid? UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
