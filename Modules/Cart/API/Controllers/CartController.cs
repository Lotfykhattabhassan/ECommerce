using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Cart.Application.DTOs.Cart;
using MiniECommerce.Modules.Cart.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Cart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Admin only
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CartReadDto>> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartByIdAsync(
                id,
                cancellationToken);

            return Ok(cart);
        }

        // Current user
        [HttpGet("my")]
        public async Task<ActionResult<IReadOnlyCollection<CartReadDto>>> GetMyCarts(
            CancellationToken cancellationToken)
        {
            var carts = await _cartService.GetMyCartsAsync(
                cancellationToken);

            return Ok(carts);
        }

        // Current user's active cart
        [HttpGet("my/active")]
        public async Task<ActionResult<CartReadDto>> GetMyActiveCart(
            CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetMyActiveCartAsync(
                cancellationToken);

            return Ok(cart);
        }

        // Current user - specific cart
        [HttpGet("my/{cartId:guid}")]
        public async Task<ActionResult<CartReadDto>> GetMyCartById(
            Guid cartId,
            CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetCartByIdAndUserId(
                cartId,
                cancellationToken);

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            CancellationToken cancellationToken)
        {
            var cartId = await _cartService.CreateCartAsync(
                cancellationToken);

            return Ok(cartId);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            [FromBody] AddCartItemDto dto,
            CancellationToken cancellationToken)
        {
            await _cartService.AddItemAsync(
                dto,
                cancellationToken);

            return NoContent();
        }

        [HttpPut("items/quantity")]
        public async Task<IActionResult> ChangeItemQuantity(
            [FromBody] ChangeCartItemQuantityDto dto,
            CancellationToken cancellationToken)
        {
            await _cartService.ChangeItemQuantityAsync(
                dto,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("items/{productId:guid}")]
        public async Task<IActionResult> RemoveItem(
            Guid productId,
            CancellationToken cancellationToken)
        {
            await _cartService.RemoveItemAsync(
                productId,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("items")]
        public async Task<IActionResult> Clear(
            CancellationToken cancellationToken)
        {
            await _cartService.ClearCartAsync(
                cancellationToken);

            return NoContent();
        }

        [HttpPost("abandon")]
        public async Task<IActionResult> Abandon(
            CancellationToken cancellationToken)
        {
            await _cartService.AbandonCartAsync(
                cancellationToken);

            return NoContent();
        }
    }
}