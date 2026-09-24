using Contract.Cart.Dtos;

namespace Contract.Cart.Abstractions
{
    public interface IProductCatalog
    {
        Task<ProductInfo?> GetProductForCartAsync(
            Guid productId,
            CancellationToken cancellationToken = default);
    }
}
