using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Orders.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Orders.API.Controllers
{
    [ApiController]
    [Route("api/order-items")]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(
            IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("{orderItemId:guid}")]
        public async Task<IActionResult> GetById(
            Guid orderItemId,
            CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemService.GetByIdAsync(
                orderItemId,
                cancellationToken);

            return Ok(orderItem);
        }

        [HttpGet("order/{orderId:guid}")]
        public async Task<IActionResult> GetByOrderId(
            Guid orderId,
            CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemService.GetByOrderIdAsync(
                orderId,
                cancellationToken);

            return Ok(orderItems);
        }

        [HttpPut("{orderItemId:guid}/quantity")]
        public async Task<IActionResult> ChangeQuantity(
            Guid orderItemId,
            [FromQuery] int quantity,
            CancellationToken cancellationToken)
        {
            await _orderItemService.ChangeQuantityAsync(
                orderItemId,
                quantity,
                cancellationToken);

            return NoContent();
        }
    }
}