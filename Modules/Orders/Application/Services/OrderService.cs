using AutoMapper;
using Contract.Order.Abstractions;
using MiniECommerce.Modules.Orders.Application.Abstractions;
using MiniECommerce.Modules.Orders.Application.DTOs.Order;
using MiniECommerce.Modules.Orders.Application.Services.Abstractions;
using MiniECommerce.Modules.Orders.Domain.Entities;
using MiniECommerce.Modules.Orders.Application.Exceptions.Order;

namespace MiniECommerce.Modules.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartOrder _cartOrder;
        private readonly IOrderCatalog _orderCatalog;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            ICartOrder cartOrder,
            IOrderCatalog orderCatalog,
            ICurrentUser currentUser,
            IMapper mapper)
        {
            _cartOrder = cartOrder;
            _orderCatalog = orderCatalog;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task CancelAsync(Guid orderId, CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException();

            if (order.UserId != userId)
                throw new UnauthorizedAccessException();

            order.Cancel();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task CompleteAsync(Guid orderId, CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException();

            if (order.UserId != userId)
                throw new UnauthorizedAccessException();

            order.Complete();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task ConfirmAsync(Guid orderId, CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException();

            if (order.UserId != userId)
                throw new UnauthorizedAccessException();

            order.Confirm();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<Guid> CreateOrderAsync(
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var cart = await _cartOrder.GetCartInfoForOrder(
                userId.Value,
                cancellationToken);

            if (cart.Items.Count == 0)
                throw new InvalidOperationException("Cart is empty.");

            var order = Order.Create(userId.Value);

            foreach (var item in cart.Items)
            {
                var productName =
                    await _orderCatalog.GetProductNameForOrder(
                        item.ProductId,
                        cancellationToken);

                order.AddItem(
                    item.ProductId,
                    productName,
                    item.UnitPrice,
                    item.Quantity);
            }

            await _orderRepository.AddAsync(
                order,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            await _cartOrder.CheckedOutCartForOrder(userId.Value, cancellationToken);

            return order.Id;
        }

        public async Task<IReadOnlyCollection<OrderReadDto>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<OrderReadDto>>(orders);
        }

        public async Task<OrderReadDto> GetByIdAsync(Guid orderId,
            CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                throw new OrderNotFoundException();

            return _mapper.Map<OrderReadDto>(order);

        }

        public async Task<OrderReadDto> GetMyOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var order = await _orderRepository
                .GetOrderByIdAndUserIdAsync(
                orderId,
                userId.Value,
                cancellationToken);

            if (order == null)
                throw new OrderNotFoundException();

            return _mapper.Map<OrderReadDto>(order);

        }

        public async Task<IReadOnlyCollection<OrderReadDto>> GetMyOrdersAsync(CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var orders = await _orderRepository.GetByUserIdAsync(userId.Value, cancellationToken);

            return _mapper.Map<List<OrderReadDto>>(orders);
        }
    }
}
