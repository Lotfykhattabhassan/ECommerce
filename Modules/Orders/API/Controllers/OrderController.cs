using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Orders.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Orders.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            CancellationToken cancellationToken)
        {
            var orderId = await _orderService.CreateOrderAsync(
                cancellationToken);

            return CreatedAtAction(
                nameof(GetMyOrderById),
                new { orderId },
                new { orderId });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders(
            CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetMyOrdersAsync(
                cancellationToken);

            return Ok(orders);
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetMyOrderById(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _orderService.GetMyOrderByIdAsync(
                orderId,
                cancellationToken);

            return Ok(order);
        }

        [HttpPut("{orderId:guid}/confirm")]
        public async Task<IActionResult> Confirm(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            await _orderService.ConfirmAsync(
                orderId,
                cancellationToken);

            return NoContent();
        }

        [HttpPut("{orderId:guid}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            await _orderService.CancelAsync(
                orderId,
                cancellationToken);

            return NoContent();
        }

        [HttpPut("{orderId:guid}/complete")]
        public async Task<IActionResult> Complete(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            await _orderService.CompleteAsync(
                orderId,
                cancellationToken);

            return NoContent();
        }

        // Admin
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAllAsync(
                cancellationToken);

            return Ok(orders);
        }

        // Admin
        [HttpGet("admin/{orderId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var order = await _orderService.GetByIdAsync(
                orderId,
                cancellationToken);

            return Ok(order);
        }
    }
}