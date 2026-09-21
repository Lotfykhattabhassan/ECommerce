using AutoMapper;
using FluentValidation;
using MiniECommerce.Modules.Reviews.Application.Abstractions;
using MiniECommerce.Modules.Reviews.Application.DTOs;
using MiniECommerce.Modules.Reviews.Application.Exceptions;
using MiniECommerce.Modules.Reviews.Application.Services.Abstractions;
using MiniECommerce.Modules.Reviews.Domain.Entities;

namespace MiniECommerce.Modules.Reviews.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateReviewDto> _validator;
        private readonly IValidator<UpdateReviewDto> _validatorUpdate;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        public ReviewService(IReviewRepository reviewRepository,
            IUnitOfWork unitOfWork,
            IValidator<CreateReviewDto> validator,
            IValidator<UpdateReviewDto> validatorUpdate,
            ICurrentUser currentUser,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
            _currentUser = currentUser;
            _validatorUpdate = validatorUpdate;
        }

        public async Task<int> CreateReviewAsync(CreateReviewDto dto,CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));
            var result = await _validator.ValidateAsync(dto, cancellationToken);
            if (!result.IsValid)
                throw new InvalidReviewDataException();

            var review = Review.Create(dto.ProductId, userId.Value, dto.Rating, dto.Comment);

            await _reviewRepository.AddReviewAsync(review, cancellationToken);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return review.Id;
        }

        public async Task<IReadOnlyList<ReviewReadDto>> GetAllReviewsAsync(CancellationToken cancellationToken = default)
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync(cancellationToken);

            return _mapper.Map<List<ReviewReadDto>>(reviews); 
        }
        public async Task<ReviewReadDto?> GetReviewByIdAsync(int id,CancellationToken cancellationToken = default)
        {
            if(id <= 0 ) throw new InvalidReviewDataException();

            var review = await _reviewRepository.GetReviewByIdAsync(id, cancellationToken);

            if (review == null) throw new ReviewNotFoundException();

            return _mapper.Map<ReviewReadDto>(review);
        }
        public async Task<IReadOnlyList<ReviewReadDto>> GetMyReviewsAsync( CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));
            var reviews = await _reviewRepository.GetUserReviewsAsync(userId.Value, cancellationToken);

            return _mapper.Map<List<ReviewReadDto>>(reviews);
        }

        public async Task<IReadOnlyList<ReviewReadDto>> GetUserReviewsAsync(Guid userId,CancellationToken cancellationToken = default)
        {
            if (userId == Guid.Empty) throw new ArgumentException(nameof(userId));
            var reviews = await _reviewRepository.GetUserReviewsAsync(userId, cancellationToken);

            return _mapper.Map<List<ReviewReadDto>>(reviews);
        }

        public async Task<IReadOnlyList<ReviewReadDto>> GetProductReviewsAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            if (productId == Guid.Empty) throw new ArgumentException(nameof(productId));
            var reviews = await _reviewRepository.GetProductReviewsAsync(productId, cancellationToken);

            return _mapper.Map<List<ReviewReadDto>>(reviews);
        }

        public async Task<string> UpdateReviewAsync(
            UpdateReviewDto dto,
            CancellationToken cancellationToken = default)
        {
            var result = await _validatorUpdate.ValidateAsync(dto, cancellationToken);

            if (!result.IsValid)
                throw new InvalidReviewDataException();

            var review = await _reviewRepository
                .GetReviewByIdAsync(dto.Id, cancellationToken);

            if (review == null)
                throw new ReviewNotFoundException();

            var isAdmin = _currentUser.Roles.Contains("Admin");

            if (!isAdmin && review.UserId != _currentUser.UserId)
                throw new UnauthorizedAccessException();

            review.ChangeRating(dto.Rating);
            review.ChangeComment(dto.Comment);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return "Review Updated Successfully";
        }

        public async Task<string> DeleteReviewAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new InvalidReviewDataException();

            var review = await _reviewRepository
                .GetReviewByIdAsync(id, cancellationToken);

            if (review == null)
                throw new ReviewNotFoundException();

            var isAdmin = _currentUser.Roles.Contains("Admin");

            if (!isAdmin && review.UserId != _currentUser.UserId)
                throw new UnauthorizedAccessException();

            review.MarkAsDeleted();

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return "Review Deleted Successfully";
        }
    }
}
