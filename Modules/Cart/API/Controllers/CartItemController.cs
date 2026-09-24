using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Cart.Application.DTOs.CartItem;
using MiniECommerce.Modules.Cart.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("{cartItemId:guid}")]
        public async Task<ActionResult<CartItemReadDto>> GetById(
            Guid cartItemId,
            CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemService.GetByIdAsync(
                cartItemId,
                cancellationToken);

            return Ok(cartItem);
        }

        [HttpGet("cart/{cartId:guid}")]
        public async Task<ActionResult<IReadOnlyCollection<CartItemReadDto>>> GetByCartId(
            Guid cartId,
            CancellationToken cancellationToken)
        {
            var cartItems = await _cartItemService.GetByCartIdAsync(
                cartId,
                cancellationToken);

            return Ok(cartItems);
        }

        [HttpPatch("{cartItemId:guid}/increase")]
        public async Task<IActionResult> IncreaseQuantity(
            Guid cartItemId,
            [FromQuery] int quantity,
            CancellationToken cancellationToken)
        {
            await _cartItemService.IncreaseQuantityAsync(
                cartItemId,
                quantity,
                cancellationToken);

            return NoContent();
        }

        [HttpPatch("{cartItemId:guid}/decrease")]
        public async Task<IActionResult> DecreaseQuantity(
            Guid cartItemId,
            [FromQuery] int quantity,
            CancellationToken cancellationToken)
        {
            await _cartItemService.DecreaseQuantityAsync(
                cartItemId,
                quantity,
                cancellationToken);

            return NoContent();
        }

        [HttpPut("{cartItemId:guid}/quantity")]
        public async Task<IActionResult> ChangeQuantity(
            Guid cartItemId,
            [FromQuery] int quantity,
            CancellationToken cancellationToken)
        {
            await _cartItemService.ChangeQuantityAsync(
                cartItemId,
                quantity,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> Remove(
            Guid cartItemId,
            CancellationToken cancellationToken)
        {
            await _cartItemService.RemoveAsync(
                cartItemId,
                cancellationToken);

            return NoContent();
        }
    }
}