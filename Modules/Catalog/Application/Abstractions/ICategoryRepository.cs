using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Abstractions
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category category, CancellationToken cancellationToken = default);
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Category>> GetCategoriesByParentAsync(Guid parentCategoryId,
            CancellationToken cancellationToken = default);
        Task<Category?> GetDeletedCategoryByIdAsync(
            Guid id,
            CancellationToken cancellationToken);
    }
}
