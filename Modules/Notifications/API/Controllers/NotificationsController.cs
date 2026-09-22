using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MiniECommerce.Modules.Notifications.Application.DTOs;
using MiniECommerce.Modules.Notifications.Application.Services;
using MiniECommerce.Modules.Notifications.Domain.Enums;
using System.Text.RegularExpressions;

namespace MiniECommerce.Modules.Notifications.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationsController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNotification(
            [FromBody] CreateNotificationDto dto,
            CancellationToken cancellationToken)
        {
            var notificationId =
                await _notificationService.CreateNotificationAsync(
                    dto,
                    cancellationToken);

            return CreatedAtAction(
                nameof(GetNotificationById),
                new { id = notificationId },
                notificationId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyCollection<NotificationReadDto>>> GetAllNotifications(
            CancellationToken cancellationToken)
        {
            var notifications =
                await _notificationService.GetAllNotificationsAsync(
                    cancellationToken);

            return Ok(notifications);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotificationReadDto>> GetNotificationById(
            int id,
            CancellationToken cancellationToken)
        {
            var notification =
                await _notificationService.GetNotificationByIdAsync(
                    id,
                    cancellationToken);

            return Ok(notification);
        }

        [HttpGet("my")]
        public async Task<ActionResult<IReadOnlyCollection<NotificationReadDto>>> GetMyNotifications(
            CancellationToken cancellationToken)
        {
            var notifications =
                await _notificationService.GetMyNotificationsAsync(
                    cancellationToken);

            return Ok(notifications);
        }

        [HttpGet("user/{userId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyCollection<NotificationReadDto>>> GetNotificationsByUserId(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var notifications =
                await _notificationService.GetNotificationsByUserIdAsync(
                    userId,
                    cancellationToken);

            return Ok(notifications);
        }

        [HttpGet("type/{type}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyCollection<NotificationReadDto>>> GetNotificationsByType(
            NotificationType type,
            CancellationToken cancellationToken)
        {
            var notifications =
                await _notificationService.GetNotificationsByTypeAsync(
                    type,
                    cancellationToken);

            return Ok(notifications);
        }

        [HttpPatch("{id:int}/read")]
        public async Task<IActionResult> MarkNotificationAsRead(
            int id,
            CancellationToken cancellationToken)
        {
            await _notificationService.MarkNotificationAsReadAsync(
                id,
                cancellationToken);

            return Ok("Notification marked as read successfully.");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(
            int id,
            CancellationToken cancellationToken)
        {
            await _notificationService.DeleteNotificationAsync(
                id,
                cancellationToken);

            return Ok("Notification deleted successfully.");
        }
    }
}