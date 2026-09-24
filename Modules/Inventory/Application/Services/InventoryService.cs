using Contract.Cart.Abstractions;
using FluentValidation;
using MiniECommerce.Modules.Inventory.Application.Abstractions;
using MiniECommerce.Modules.Inventory.Application.DTOs;
using MiniECommerce.Modules.Inventory.Application.Exceptions;
using MiniECommerce.Modules.Inventory.Application.Services.Abstractions;
using MiniECommerce.Modules.Inventory.Domain.Entities;

namespace MiniECommerce.Modules.Inventory.Application.Services
{
    public class InventoryService : IInventoryService, IProductInventory
    {
        private readonly IProductInventoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateInventoryDto> _createValidator;
        private readonly IValidator<UpdateStockDto> _updateValidator;
        private readonly IValidator<ReserveStockDto> _reserveValidator;

        public InventoryService(
            IProductInventoryRepository repository,
            IUnitOfWork unitOfWork,
            IValidator<CreateInventoryDto> createValidator,
            IValidator<UpdateStockDto> updateValidator,
            IValidator<ReserveStockDto> reserveValidator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _reserveValidator = reserveValidator;
        }

        public async Task<Guid> CreateInventoryAsync(
            CreateInventoryDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _createValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidInventoryDataException();

            var existing = await _repository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (existing != null)
                throw new InventoryAlreadyExistsException();

            var deleted = await _repository.GetDeletedByProductIdAsync(dto.ProductId, cancellationToken);
            if (deleted != null)
                throw new InventoryAlreadyExistsException("Inventory already exists for this product and is deleted.");

            var inventory = ProductInventory.Create(dto.ProductId, dto.Quantity);
            await _repository.AddAsync(inventory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return inventory.Id;
        }

        public async Task<InventoryReadDto> GetByProductIdAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            ValidateProductId(productId);

            var inventory = await _repository.GetByProductIdAsync(productId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            return Map(inventory);
        }

        public async Task<IReadOnlyCollection<InventoryReadDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var inventories = await _repository.GetAllAsync(cancellationToken);
            return inventories.Select(Map).ToList();
        }

        public async Task AddStockAsync(
            UpdateStockDto dto,
            CancellationToken cancellationToken = default)
        {
            await UpdateStockAsync(dto, (inventory, quantity) => inventory.AddStock(quantity), cancellationToken);
        }

        public async Task RemoveStockAsync(
            UpdateStockDto dto,
            CancellationToken cancellationToken = default)
        {
            await UpdateStockAsync(dto, (inventory, quantity) => inventory.RemoveStock(quantity), cancellationToken);
        }

        public async Task ReserveStockAsync(
            ReserveStockDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _reserveValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidInventoryDataException();

            var inventory = await _repository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            inventory.ReserveStock(dto.Quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ReleaseReservedStockAsync(
            ReserveStockDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _reserveValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidInventoryDataException();

            var inventory = await _repository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            inventory.ReleaseReservedStock(dto.Quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteInventoryAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            ValidateProductId(productId);

            var inventory = await _repository.GetByProductIdAsync(productId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            inventory.MarkAsDeleted();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RestoreInventoryAsync(
            Guid productId,
            CancellationToken cancellationToken = default)
        {
            ValidateProductId(productId);

            var inventory = await _repository.GetDeletedByProductIdAsync(productId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            inventory.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task UpdateStockAsync(
            UpdateStockDto dto,
            Action<ProductInventory, int> action,
            CancellationToken cancellationToken)
        {
            var result = await _updateValidator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidInventoryDataException();

            var inventory = await _repository.GetByProductIdAsync(dto.ProductId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            action(inventory, dto.Quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static void ValidateProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new InvalidInventoryDataException();
        }

        private static InventoryReadDto Map(ProductInventory inventory)
        {
            return new InventoryReadDto(
                inventory.Id,
                inventory.ProductId,
                inventory.Quantity,
                inventory.ReservedQuantity,
                inventory.GetAvailableQuantity());
        }

        public async Task<bool> CheckProductQuantityAvailabilityForCartAsync(Guid productId, int quantity, CancellationToken cancellationToken)
        {
            ValidateProductId(productId);

            var inventory = await _repository.GetByProductIdAsync(productId, cancellationToken);
            if (inventory == null)
                throw new InventoryNotFoundException();

            return inventory.HasAvailableQuantity(quantity);
        }
    }
}
