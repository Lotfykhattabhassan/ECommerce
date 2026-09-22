
using MiniECommerce.Modules.Catalog.Application.DTOs.Product;

namespace MiniECommerce.Modules.Catalog.Application.Services.Abstractions
{
    public interface IProductService
    {
        Task<Guid> CreateProductAsync(CreateProductDto dto,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<ProductReadDto>> GetAllProductsAsync(
            CancellationToken cancellationToken = default);
        Task<ProductReadDto> GetProductByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<ProductReadDto>> GetProductsByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default);
        Task<ProductReadDto> GetProductBySKUAsync(string sku,
            CancellationToken cancellationToken = default);
        Task UpdateProductInformationAsync(UpdateProductDto dto,
            CancellationToken cancellationToken = default);
        Task ActivateProductAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task DeactivateProductAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task DeleteProductAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task RestoreProductAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
