using AutoMapper;
using Contract.Cart.Abstractions;
using Contract.Cart.Dtos;
using Contract.Order.Abstractions;
using FluentValidation;
using MiniECommerce.Modules.Catalog.Application.Abstractions;
using MiniECommerce.Modules.Catalog.Application.DTOs.Product;
using MiniECommerce.Modules.Catalog.Application.Exceptions.Category;
using MiniECommerce.Modules.Catalog.Application.Exceptions.Product;
using MiniECommerce.Modules.Catalog.Application.Services.Abstractions;
using MiniECommerce.Modules.Catalog.Domain.Entities;

namespace MiniECommerce.Modules.Catalog.Application.Services
{
    public class ProductService : IProductService, IProductCatalog, IOrderCatalog
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _validator;
        private readonly IValidator<UpdateProductDto> _validatorUpdate;
        public ProductService(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
           IUnitOfWork unitOfWork,
           IMapper mapper,
           IValidator<CreateProductDto> validator,
           IValidator<UpdateProductDto> validatorUpdate)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
            _validatorUpdate = validatorUpdate;
        }

        public async Task<Guid> CreateProductAsync(CreateProductDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidProductDataException();
            
            var category = await _categoryRepository.GetCategoryByIdAsync(
                dto.CategoryId,
                cancellationToken);

            if (category is null)
                throw new CategoryNotFoundException();

            var existingProduct = await _productRepository.GetProductBySkuAsync(
                dto.SKU,
                cancellationToken);

            if (existingProduct != null)
                throw new ProductSkuAlreadyExistsException();

            var product = Product.Create(dto.Name, dto.Description, dto.Price, dto.SKU, dto.CategoryId);
            await _productRepository.AddProductAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
        public async Task<IReadOnlyCollection<ProductReadDto>> GetAllProductsAsync(
            CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAllProductsAsync(cancellationToken);

            return _mapper.Map<List<ProductReadDto>>(products);
        }
        public async Task<ProductReadDto> GetProductByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if(id == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            return _mapper.Map<ProductReadDto>(product);
        }
        public async Task<IReadOnlyCollection<ProductReadDto>> GetProductsByCategoryAsync(
            Guid categoryId,
            CancellationToken cancellationToken = default)
        {

            if (categoryId == Guid.Empty)
                throw new InvalidProductDataException();

            var products = await _productRepository.GetProductsByCategory(categoryId, cancellationToken);

            return _mapper.Map<List<ProductReadDto>>(products);
        }
        public async Task<ProductReadDto> GetProductBySKUAsync(string sku,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new InvalidProductDataException();
            var product = await _productRepository.GetProductBySkuAsync(sku, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            return _mapper.Map<ProductReadDto>(product);
        }
        public async Task UpdateProductInformationAsync(UpdateProductDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorUpdate.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidProductDataException();

            var category = await _categoryRepository.GetCategoryByIdAsync(
                dto.CategoryId,
                cancellationToken);

            if (category is null)
                throw new CategoryNotFoundException();

            var existingProduct = await _productRepository.GetProductBySkuAsync(
                dto.SKU,
                cancellationToken);

            if (existingProduct != null && existingProduct.Id != dto.Id)
                throw new ProductSkuAlreadyExistsException();

            var product = await _productRepository.GetProductByIdAsync(dto.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();
            product.ChangeProductName(dto.Name);
            product.ChangeProductDescription(dto.Description);
            product.ChangeProductCategory(dto.CategoryId);
            product.ChangeProductSKU(dto.SKU);
            product.ChangeProductPrice(dto.Price);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task ActivateProductAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            product.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeactivateProductAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            product.Deactivate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteProductAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            product.MarkAsDeleted();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task RestoreProductAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetDeletedProductByIdAsync(id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            product.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductInfo?> GetProductForCartAsync(Guid productId, CancellationToken cancellationToken)
        {
            if (productId == Guid.Empty)
                throw new InvalidProductDataException();

            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            return new ProductInfo(productId, product.Price, product.Status == Domain.Enums.ProductStatus.Active);
        }

        public async Task<string> GetProductNameForOrder(Guid productId, CancellationToken cancellationToken)
        {
            if (productId == Guid.Empty)
                throw new InvalidProductDataException();
            
            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            return product.Name;
        }
    }
}
