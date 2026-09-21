
namespace MiniECommerce.Modules.Reviews.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
