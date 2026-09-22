using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Catalog.Application.DTOs.Category;
using MiniECommerce.Modules.Catalog.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateCategoryDto dto,
            CancellationToken cancellationToken)
        {
            var categoryId = await _categoryService.CreateCategoryAsync(
                dto,
                cancellationToken);

            return CreatedAtAction(
                nameof(GetById),
                new { id = categoryId },
                new
                {
                    id = categoryId,
                    message = "Category created successfully."
                });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetCategoryByIdAsync(
                id,
                cancellationToken);

            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(
                cancellationToken);

            return Ok(categories);
        }

        [HttpGet("parent/{parentId:guid}")]
        public async Task<IActionResult> GetByParent(
            Guid parentId,
            CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetCategoriesByParentAsync(
                parentId,
                cancellationToken);

            return Ok(categories);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateCategoryDto dto,
            CancellationToken cancellationToken)
        {
            // لو الـ DTO عندك فيه Id، نخليه متوافق مع الـ route
            dto.Id = id;

            await _categoryService.UpdateCategoryAsync(
                dto,
                cancellationToken);

            return Ok(new
            {
                message = "Category updated successfully."
            });
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _categoryService.DeleteCategoryAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Category deleted successfully."
            });
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(
            Guid id,
            CancellationToken cancellationToken)
        {
            await _categoryService.RestoreCategoryAsync(
                id,
                cancellationToken);

            return Ok(new
            {
                message = "Category restored successfully."
            });
        }
    }
}