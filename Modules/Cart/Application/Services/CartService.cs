using AutoMapper;
using Contract.Cart.Abstractions;
using Contract.Order.Abstractions;
using Contract.Order.Dtos;
using FluentValidation;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Application.DTOs.Cart;
using MiniECommerce.Modules.Cart.Application.Exceptions;
using MiniECommerce.Modules.Cart.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Cart.Application.Services
{
    public class CartService : ICartService, ICartOrder
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IValidator<AddCartItemDto> _validator;
        private readonly IProductCatalog _productCatalog;
        private readonly IProductInventory _productInventory;
        private readonly IValidator<ChangeCartItemQuantityDto> _validatorChange;
        public CartService(ICartRepository cartRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            ICartItemRepository cartItemRepository,
            IMapper mapper,
            IValidator<AddCartItemDto> validator,
            IProductCatalog productCatalog,
            IProductInventory productInventory,
            IValidator<ChangeCartItemQuantityDto> validatorChange)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _validator = validator;
            _productCatalog = productCatalog;
            _productInventory = productInventory;
            _validatorChange = validatorChange;
        }
        public async Task<Guid> CreateCartAsync(CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = Domain.Entities.Cart.Create(userId.Value);

            await _cartRepository.AddAsync(cart, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return cart.Id;
        }
        public async Task<CartReadDto> GetCartByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id));

            var cart = await _cartRepository.GetByIdAsync(id, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            return _mapper.Map<CartReadDto>(cart);
        }
        public async Task<IReadOnlyCollection<CartReadDto>> GetMyCartsAsync(
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var carts = await _cartRepository.GetByUserIdAsync(userId.Value, cancellationToken);

            return _mapper.Map<List<CartReadDto>>(carts);
        }

        public async Task AbandonCartAsync(CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            cart.MarkAsAbandoned();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task AddItemAsync(AddCartItemDto dto, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new InvalidCartItemDataException();

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            var isNewProduct = !cart.CartItems.Any(x => x.ProductId == dto.ProductId);

            var productInfo = await _productCatalog.GetProductForCartAsync(dto.ProductId, cancellationToken);
            if (!productInfo.IsActive)
                throw new ProductIsNotActiveException();

            var quantityAvailability = await _productInventory
                .CheckProductQuantityAvailabilityForCartAsync(dto.ProductId, dto.Quantity, cancellationToken);
            if (!quantityAvailability)
                throw new UnavailableQuantityException();

            cart.AddItem(dto.ProductId, productInfo.Price, dto.Quantity);

            if (isNewProduct)
            {
                var newItem = cart.CartItems.First(x => x.ProductId == dto.ProductId);
                await _cartItemRepository.AddAsync(newItem, cancellationToken); // تسجيل صريح كـ Added
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ChangeItemQuantityAsync(ChangeCartItemQuantityDto dto, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var result = await _validatorChange.ValidateAsync(dto);
            if (!result.IsValid)
                throw new InvalidCartItemDataException();

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            var quantityAvailability = await _productInventory
                .CheckProductQuantityAvailabilityForCartAsync(dto.ProductId, dto.Quantity, cancellationToken);

            if (!quantityAvailability)
                throw new UnavailableQuantityException();

            cart.ChangeItemQuantity(dto.ProductId, dto.Quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ClearCartAsync(CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            cart.Clear();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<CartReadDto> GetMyActiveCartAsync(CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            return _mapper.Map<CartReadDto>(cart);
        }
        public async Task<CartReadDto> GetCartByIdAndUserId(
            Guid cartId,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));
            
            if (cartId == Guid.Empty)
                throw new ArgumentException(nameof(cartId));

            var cart = await _cartRepository.GetByIdAndUserIdAsync(cartId, userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            return _mapper.Map<CartReadDto>(cart);
        }
        public async Task RemoveItemAsync(Guid productId, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));

            var cart = await _cartRepository.GetActiveByUserIdAsync(userId.Value, cancellationToken);
            if (cart == null)
                throw new CartNotFoundException();

            cart.RemoveItem(productId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<CartReadForOrderDto> GetCartInfoForOrder(
            Guid userId,
            CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository
                .GetActiveByUserIdAsync(userId, cancellationToken);

            if (cart == null)
                throw new CartNotFoundException();

            var items = cart.CartItems
                .Select(x => new CartItemForOrderDto(
                    x.ProductId,
                    x.Quantity,
                    x.UnitPrice))
                .ToList();

            return new CartReadForOrderDto(items);
        }

        public async Task CheckedOutCartForOrder(Guid userId, CancellationToken cancellationToken)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository
                .GetActiveByUserIdAsync(userId, cancellationToken);

            if (cart == null)
                throw new CartNotFoundException();

            cart.MarkAsCheckedOut();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
