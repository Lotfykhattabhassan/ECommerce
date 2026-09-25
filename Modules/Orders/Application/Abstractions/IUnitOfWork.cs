namespace MiniECommerce.Modules.Orders.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}