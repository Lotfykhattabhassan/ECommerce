namespace Contract.Order.Abstractions
{
    public interface IOrderCatalog
    {
        Task<string> GetProductNameForOrder(Guid productId,
            CancellationToken cancellationToken = default);
    }
}
