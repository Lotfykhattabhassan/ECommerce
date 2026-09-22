using AutoMapper;
using FluentValidation;
using MiniECommerce.Modules.Notifications.Application.Abstractions;
using MiniECommerce.Modules.Notifications.Application.DTOs;
using MiniECommerce.Modules.Notifications.Application.Exceptions;
using MiniECommerce.Modules.Notifications.Application.Services.Abstraction;
using MiniECommerce.Modules.Notifications.Domain.Entities;
using MiniECommerce.Modules.Notifications.Domain.Enums;

namespace MiniECommerce.Modules.Notifications.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IValidator<CreateNotificationDto> _validator;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUser currentUser,
            IValidator<CreateNotificationDto> validator)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _validator = validator;
        }

        public async Task<int> CreateNotificationAsync(
            CreateNotificationDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validator.ValidateAsync(dto, cancellationToken);

            if (!result.IsValid)
                throw new InvalidNotificationDataException();

            var notification = Notification.Create(
                dto.UserId,
                dto.Title,
                dto.Message,
                dto.Type);

            await _notificationRepository.AddNotificationAsync(
                notification,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return notification.Id;
        }

        public async Task<IReadOnlyCollection<NotificationReadDto>> GetAllNotificationsAsync(
            CancellationToken cancellationToken = default)
        {
            var notifications =
                await _notificationRepository.GetAllNotificationsAsync(
                    cancellationToken);

            return _mapper.Map<List<NotificationReadDto>>(notifications);
        }

        public async Task<NotificationReadDto> GetNotificationByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidNotificationDataException();

            var notification =
                await _notificationRepository.GetNotificationByIdAsync(
                    id,
                    cancellationToken);

            if (notification == null)
                throw new NotificationNotFoundException();

            if (!IsOwnerOrAdmin(notification.UserId))
                throw new UnauthorizedAccessException();

            return _mapper.Map<NotificationReadDto>(notification);
        }

        public async Task<IReadOnlyCollection<NotificationReadDto>> GetNotificationsByUserIdAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty)
                throw new InvalidNotificationDataException();

            var notifications =
                await _notificationRepository.GetUserNotificationsAsync(
                    userId,
                    cancellationToken);

            return _mapper.Map<List<NotificationReadDto>>(notifications);
        }

        public async Task<IReadOnlyCollection<NotificationReadDto>> GetMyNotificationsAsync(
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;

            if (!userId.HasValue || userId.Value == Guid.Empty)
                throw new InvalidNotificationDataException();

            var notifications =
                await _notificationRepository.GetUserNotificationsAsync(
                    userId.Value,
                    cancellationToken);

            return _mapper.Map<List<NotificationReadDto>>(notifications);
        }

        public async Task<IReadOnlyCollection<NotificationReadDto>> GetNotificationsByTypeAsync(
            NotificationType type,
            CancellationToken cancellationToken = default)
        {
            var notifications =
                await _notificationRepository.GetNotificationsByTypeAsync(
                    type,
                    cancellationToken);

            return _mapper.Map<List<NotificationReadDto>>(notifications);
        }

        public async Task MarkNotificationAsReadAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidNotificationDataException();

            var notification =
                await _notificationRepository.GetNotificationByIdAsync(
                    id,
                    cancellationToken);

            if (notification == null)
                throw new NotificationNotFoundException();

            if (!IsOwnerOrAdmin(notification.UserId))
                throw new UnauthorizedAccessException();

            notification.MarkAsRead();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteNotificationAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidNotificationDataException();

            var notification =
                await _notificationRepository.GetNotificationByIdAsync(
                    id,
                    cancellationToken);

            if (notification == null)
                throw new NotificationNotFoundException();

            if (!IsOwnerOrAdmin(notification.UserId))
                throw new UnauthorizedAccessException();

            notification.MarkAsDeleted();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private bool IsOwnerOrAdmin(Guid notificationUserId)
        {
            var currentUserId = _currentUser.UserId;

            if (currentUserId.HasValue &&
                currentUserId.Value == notificationUserId)
            {
                return true;
            }

            return _currentUser.Roles.Contains("Admin");
        }
    }
}
