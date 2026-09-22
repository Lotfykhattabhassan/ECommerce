using MiniECommerce.Modules.Catalog.Application.DTOs.Category;

namespace MiniECommerce.Modules.Catalog.Application.Services.Abstractions
{
    public interface ICategoryService
    {
        Task<Guid> CreateCategoryAsync(CreateCategoryDto dto,
            CancellationToken cancellationToken = default);
        Task<CategoryReadDto> GetCategoryByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<CategoryReadDto>> GetAllCategoriesAsync(
            CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<CategoryReadDto>> GetCategoriesByParentAsync(
            Guid parentId,
            CancellationToken cancellationToken = default);
        Task UpdateCategoryAsync(UpdateCategoryDto dto,
            CancellationToken cancellationToken = default);
        Task DeleteCategoryAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task RestoreCategoryAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
