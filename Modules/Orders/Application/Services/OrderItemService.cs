using AutoMapper;
using MiniECommerce.Modules.Orders.Application.Abstractions;
using MiniECommerce.Modules.Orders.Application.DTOs.OrderItem;
using MiniECommerce.Modules.Orders.Application.Exceptions.Order;
using MiniECommerce.Modules.Orders.Application.Exceptions.OrderItem;
using MiniECommerce.Modules.Orders.Application.Services.Abstractions;
using MiniECommerce.Modules.Orders.Domain.Entities;

namespace MiniECommerce.Modules.Orders.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task ChangeQuantityAsync(
            Guid orderItemId,
            int quantity,
            CancellationToken cancellationToken)
        {
            if (orderItemId == Guid.Empty)
                throw new ArgumentException(nameof(orderItemId));

            var userId = _currentUser.UserId;

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            if (quantity <= 0)
                throw new InvalidOrderItemDataException();

            var orderItem = await _orderItemRepository.GetByIdAsync(
                orderItemId,
                cancellationToken);

            if (orderItem == null)
                throw new OrderItemNotFoundException();

            var order = await _orderRepository.GetOrderByIdAndUserIdAsync(
                orderItem.OrderId,
                userId.Value,
                cancellationToken);

            if (order == null)
                throw new OrderNotFoundException();

            orderItem.ChangeQuantity(quantity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<OrderItemReadDto> GetByIdAsync(
            Guid orderItemId,
            CancellationToken cancellationToken)
        {
            if (orderItemId == Guid.Empty)
                throw new ArgumentException(nameof(orderItemId));

            var userId = _currentUser.UserId;

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var orderItem = await _orderItemRepository.GetByIdAsync(
                orderItemId,
                cancellationToken);

            if (orderItem == null)
                throw new OrderItemNotFoundException();

            var order = await _orderRepository.GetOrderByIdAndUserIdAsync(
                orderItem.OrderId,
                userId.Value,
                cancellationToken);

            if (order == null)
                throw new OrderNotFoundException();

            return _mapper.Map<OrderItemReadDto>(orderItem);
        }

        public async Task<IReadOnlyCollection<OrderItemReadDto>> GetByOrderIdAsync(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            if (orderId == Guid.Empty)
                throw new ArgumentException(nameof(orderId));

            var userId = _currentUser.UserId;

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));

            var order = await _orderRepository.GetOrderByIdAndUserIdAsync(
                orderId,
                userId.Value,
                cancellationToken);

            if (order == null)
                throw new OrderNotFoundException();

            var orderItems = await _orderItemRepository.GetByOrderIdAsync(
                orderId,
                cancellationToken);

            return _mapper.Map<List<OrderItemReadDto>>(orderItems);
        }
    }
}