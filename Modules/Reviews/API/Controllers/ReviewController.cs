using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniECommerce.Modules.Reviews.Application.DTOs;
using MiniECommerce.Modules.Reviews.Application.Services.Abstractions;

namespace MiniECommerce.Modules.Reviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ReviewReadDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetAllReviewsAsync(cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReviewReadDto>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            var review = await _reviewService.GetReviewByIdAsync(
                id,
                cancellationToken);

            return Ok(review);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<IReadOnlyList<ReviewReadDto>>> GetMyReviews(
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetMyReviewsAsync(cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ReviewReadDto>>> GetUserReviews(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetUserReviewsAsync(
                userId,
                cancellationToken);

            return Ok(reviews);
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ReviewReadDto>>> GetProductReviews(
            Guid productId,
            CancellationToken cancellationToken)
        {
            var reviews = await _reviewService.GetProductReviewsAsync(
                productId,
                cancellationToken);

            return Ok(reviews);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateReviewDto dto,
            CancellationToken cancellationToken)
        {
            var reviewId = await _reviewService.CreateReviewAsync(
                dto,
                cancellationToken);

            return Ok(reviewId);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<string>> Update(
            [FromBody] UpdateReviewDto dto,
            CancellationToken cancellationToken)
        {
            var result = await _reviewService.UpdateReviewAsync(
                dto,
                cancellationToken);

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<string>> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            var result = await _reviewService.DeleteReviewAsync(
                id,
                cancellationToken);

            return Ok(result);
        }
    }
}
