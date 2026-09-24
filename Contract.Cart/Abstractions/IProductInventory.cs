namespace Contract.Cart.Abstractions
{
    public interface IProductInventory
    {
        Task<bool> CheckProductQuantityAvailabilityForCartAsync(Guid productId,
            int quantity,
            CancellationToken cancellationToken = default);
    }
}
