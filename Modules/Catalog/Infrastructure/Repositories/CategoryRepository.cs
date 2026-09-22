using Microsoft.EntityFrameworkCore;
using MiniECommerce.Modules.Catalog.Application.Abstractions;
using MiniECommerce.Modules.Catalog.Domain.Entities;
using MiniECommerce.Modules.Catalog.Infrastructure.Persistence;

namespace MiniECommerce.Modules.Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;
        public CategoryRepository(CatalogDbContext context)
        {
            _context = context;
        }
        public async Task AddCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(category);
            await _context.Categories.AddAsync(category, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            return await _context.Categories.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Category>> GetCategoriesByParentAsync(Guid parentCategoryId, CancellationToken cancellationToken)
        {
            if (parentCategoryId == Guid.Empty)
                throw new ArgumentException(nameof(parentCategoryId));

            return await _context.Categories
                .Where(x => x.ParentCategoryId == parentCategoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<Category?> GetDeletedCategoryByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            return await _context.Categories
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
