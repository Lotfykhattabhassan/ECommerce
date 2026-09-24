using AutoMapper;
using Contract.Cart.Abstractions;
using MiniECommerce.Modules.Cart.Application.Abstractions;
using MiniECommerce.Modules.Cart.Application.DTOs.CartItem;
using MiniECommerce.Modules.Cart.Application.Exceptions;
using MiniECommerce.Modules.Cart.Application.Services.Abstractions;
namespace MiniECommerce.Modules.Cart.Application.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductInventory _productInventory;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly ICartRepository _cartRepository;
        public CartItemService(ICartItemRepository cartItemRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IProductInventory productInventory,
            IMapper mapper,
            ICartRepository cartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _unitOfWork = unitOfWork;
            _productInventory = productInventory;
            _mapper = mapper;
            _currentUser = currentUser;
            _cartRepository = cartRepository;
        }

        public async Task ChangeQuantityAsync(Guid cartItemId,
            int quantity,
            CancellationToken cancellationToken)
        {
            if (cartItemId == Guid.Empty)
                throw new InvalidCartItemDataException();
            if (quantity <= 0)
                throw new InvalidCartItemDataException();

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId, cancellationToken);
            if (cartItem == null)
                throw new CartItemNotFoundException();

            var userId = _currentUser.UserId;
            if(userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (cartItem.Cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            var isAvailable = await _productInventory
                .CheckProductQuantityAvailabilityForCartAsync(cartItem.ProductId, quantity, cancellationToken);

            if (!isAvailable)
                throw new UnavailableQuantityException();

            cartItem.ChangeQuantity(quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DecreaseQuantityAsync(Guid cartItemId, int quantity, CancellationToken cancellationToken)
        {
            if (cartItemId == Guid.Empty)
                throw new InvalidCartItemDataException();
            if (quantity <= 0)
                throw new InvalidCartItemDataException();

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId, cancellationToken);
            if (cartItem == null)
                throw new CartItemNotFoundException();
            
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (cartItem.Cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            cartItem.DecreaseQuantity(quantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<CartItemReadDto>> GetByCartIdAsync(
            Guid cartId,
            CancellationToken cancellationToken)
        {
            if (cartId == Guid.Empty)
                throw new InvalidCartItemDataException();

            var userId = _currentUser.UserId;

            if (!userId.HasValue || userId.Value == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartRepository.GetByIdAsync(
                cartId,
                cancellationToken);

            if (cart == null)
                throw new CartNotFoundException();

            if (cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            var cartItems = await _cartItemRepository
                .GetByCartIdAsync(cartId, cancellationToken);

            return _mapper.Map<List<CartItemReadDto>>(cartItems);
        }

        public async Task<CartItemReadDto> GetByIdAsync(Guid cartItemId, CancellationToken cancellationToken)
        {
            if (cartItemId == Guid.Empty)
                throw new InvalidCartItemDataException();

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId, cancellationToken);
            if (cartItem == null)
                throw new CartItemNotFoundException();
            
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (cartItem.Cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            return _mapper.Map<CartItemReadDto>(cartItem);
        }

        public async Task IncreaseQuantityAsync(
            Guid cartItemId,
            int quantity,
            CancellationToken cancellationToken)
        {
            if (cartItemId == Guid.Empty)
                throw new InvalidCartItemDataException();

            if (quantity <= 0)
                throw new InvalidCartItemDataException();

            var cartItem = await _cartItemRepository
                .GetByIdAsync(cartItemId, cancellationToken);

            if (cartItem == null)
                throw new CartItemNotFoundException();
           
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (cartItem.Cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            var newQuantity = cartItem.Quantity + quantity;

            var isAvailable = await _productInventory
                .CheckProductQuantityAvailabilityForCartAsync(
                    cartItem.ProductId,
                    newQuantity,
                    cancellationToken);

            if (!isAvailable)
                throw new UnavailableQuantityException();

            cartItem.IncreaseQuantity(quantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveAsync(Guid cartItemId, CancellationToken cancellationToken)
        {
            if (cartItemId == Guid.Empty)
                throw new InvalidCartItemDataException();

            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId, cancellationToken);
            if (cartItem == null)
                throw new CartItemNotFoundException();

            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (cartItem.Cart.UserId != userId.Value)
                throw new UnauthorizedAccessException();

            cartItem.MarkAsDeleted();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
