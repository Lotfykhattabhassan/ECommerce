using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Catalog.Application.DTOs.Product;
using MiniECommerce.Modules.Catalog.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductDto dto,
            CancellationToken cancellationToken)
        {
            var productId = await _productService.CreateProductAsync(
                dto,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = productId },
                new
                {
                    id = productId,
                    message = "Product created successfully."
                });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(
                id,
                cancellationToken);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var products = await _productService.GetAllProductsAsync(
                cancellationToken);

            return Ok(products);
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetByCategory(
            Guid categoryId,
            CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsByCategoryAsync(
                categoryId,
                cancellationToken);

            return Ok(products);
        }

        [HttpGet("sku/{sku}")]
        public async Task<IActionResult> GetBySku(
            string sku,
            CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductBySKUAsync(
                sku,
                cancellationToken);

            return Ok(product);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateProductDto dto,
            CancellationToken cancellationToken)
        {
            dto.Id = id;

            await _productService.UpdateProductInformationAsync(
                dto,
                cancellationToken);

            return Ok(new
            {
                message = "Product updated successfully."
            });
        }

        [HttpPatch("{id:guid}/activate")]
        public async Task<IActionResult> Activate(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _productService.ActivateProductAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Product activated successfully."
            });
        }

        [HttpPatch("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _productService.DeactivateProductAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Product deactivated successfully."
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _productService.DeleteProductAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Product deleted successfully."
            });
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _productService.RestoreProductAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Product restored successfully."
            });
        }
    }
}