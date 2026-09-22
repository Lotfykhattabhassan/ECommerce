using AutoMapper;
using FluentValidation;
using MiniECommerce.Modules.Catalog.Application.Abstractions;
using MiniECommerce.Modules.Catalog.Application.DTOs.Category;
using MiniECommerce.Modules.Catalog.Application.Exceptions.Category;
using MiniECommerce.Modules.Catalog.Application.Services.Abstractions;
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _validator;
        private readonly IValidator<UpdateCategoryDto> _validatorUpdate;
        public CategoryService(ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CreateCategoryDto> validator,
            IValidator<UpdateCategoryDto> validatorUpdate)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }
        public async Task<Guid> CreateCategoryAsync(CreateCategoryDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidCategoryDataException();

            if (dto.ParentCategoryId.HasValue)
            {
                var parent = await _categoryRepository.GetCategoryByIdAsync(
                    dto.ParentCategoryId.Value,
                    cancellationToken);

                if (parent == null)
                    throw new CategoryNotFoundException();
            }

            var category = Category.Create(dto.Name, dto.Description, dto.ParentCategoryId);

            await _categoryRepository.AddCategoryAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
        public async Task<CategoryReadDto> GetCategoryByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidCategoryDataException();
            
            var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException();

            return _mapper.Map<CategoryReadDto>(category);
        }
        public async Task<IReadOnlyCollection<CategoryReadDto>> GetAllCategoriesAsync(
            CancellationToken cancellationToken = default)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync(cancellationToken);

            return _mapper.Map<List<CategoryReadDto>>(categories);
        }
        public async Task<IReadOnlyCollection<CategoryReadDto>> GetCategoriesByParentAsync(
            Guid parentId,
            CancellationToken cancellationToken = default)
        {
            if (parentId == Guid.Empty)
                throw new InvalidCategoryDataException();

            var categories = await _categoryRepository.GetCategoriesByParentAsync(parentId, cancellationToken);

            return _mapper.Map<List<CategoryReadDto>>(categories);
        }
        public async Task UpdateCategoryAsync(UpdateCategoryDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorUpdate.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidCategoryDataException();

            if (dto.ParentCategoryId.HasValue)
            {
                var parent = await _categoryRepository.GetCategoryByIdAsync(
                    dto.ParentCategoryId.Value,
                    cancellationToken);

                if (parent == null)
                    throw new CategoryNotFoundException();
            }

            var category = await _categoryRepository.GetCategoryByIdAsync(dto.Id, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException();

            category.ChangeCategoryName(dto.Name);
            category.ChangeCategoryDescription(dto.Description);
            category.ChangeCategoryParentCategory(dto.ParentCategoryId);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteCategoryAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidCategoryDataException();

            var category = await _categoryRepository.GetCategoryByIdAsync(id, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException();

            category.MarkAsDeleted();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task RestoreCategoryAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidCategoryDataException();

            var category = await _categoryRepository.GetDeletedCategoryByIdAsync(id, cancellationToken);
            if (category == null)
                throw new CategoryNotFoundException();

            category.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
