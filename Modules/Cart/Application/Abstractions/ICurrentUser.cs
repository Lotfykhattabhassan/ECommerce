
namespace MiniECommerce.Modules.Cart.Application.Abstractions
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
    }
}
