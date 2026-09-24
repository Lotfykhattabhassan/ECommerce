using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Inventory.Application.DTOs;
using MiniECommerce.Modules.Inventory.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Inventory.API.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyCollection<InventoryReadDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            return Ok(await _inventoryService.GetAllAsync(cancellationToken));
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<InventoryReadDto>> GetByProductId(
            Guid productId,
            CancellationToken cancellationToken)
        {
            return Ok(await _inventoryService.GetByProductIdAsync(productId, cancellationToken));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Guid>> Create(
            CreateInventoryDto dto,
            CancellationToken cancellationToken)
        {
            var id = await _inventoryService.CreateInventoryAsync(dto, cancellationToken);
            return Ok(id);
        }

        [HttpPost("add-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddStock(
            UpdateStockDto dto,
            CancellationToken cancellationToken)
        {
            await _inventoryService.AddStockAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpPost("remove-stock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveStock(
            UpdateStockDto dto,
            CancellationToken cancellationToken)
        {
            await _inventoryService.RemoveStockAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpPost("reserve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReserveStock(
            ReserveStockDto dto,
            CancellationToken cancellationToken)
        {
            await _inventoryService.ReserveStockAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpPost("release-reservation")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ReleaseReservedStock(
            ReserveStockDto dto,
            CancellationToken cancellationToken)
        {
            await _inventoryService.ReleaseReservedStockAsync(dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("product/{productId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            Guid productId,
            CancellationToken cancellationToken)
        {
            await _inventoryService.DeleteInventoryAsync(productId, cancellationToken);
            return NoContent();
        }

        [HttpPost("product/{productId:guid}/restore")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Restore(
            Guid productId,
            CancellationToken cancellationToken)
        {
            await _inventoryService.RestoreInventoryAsync(productId, cancellationToken);
            return NoContent();
        }
    }
}
