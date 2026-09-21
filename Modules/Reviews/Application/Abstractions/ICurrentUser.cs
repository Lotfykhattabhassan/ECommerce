namespace MiniECommerce.Modules.Reviews.Application.Abstractions
{
    public interface ICurrentUser
    {
        public Guid? UserId { get;}
        public IReadOnlyList<string> Roles { get; }
    }
}
