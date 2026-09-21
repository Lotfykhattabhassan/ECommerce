using MiniECommerce.BuildingBlocks.Domain.Common;

namespace MiniECommerce.Modules.Reviews.Domain.Entities
{
    public class Review : Entity<int>
    {
        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string? Comment { get; private set; }
        private Review()
        {
            
        }
        private Review(Guid productId,
            Guid userId,
           int rating,
           string? comment)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException(nameof(productId));
            ProductId = productId;

            if (userId == Guid.Empty)
                throw new ArgumentException(nameof(userId));
            UserId = userId;

            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating should be from 1 to 5", nameof(rating));
            Rating = rating;

            Comment = comment;
        }

        public static Review Create(Guid productId,
            Guid userId,
           int rating,
           string? comment)
        {
            if (comment is not null && string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException(
                    "Comment cannot be empty.",
                    nameof(comment));

            if (comment is not null && comment.Length > 1000)
                throw new ArgumentException(
                    "Comment cannot exceed 1000 characters.",
                    nameof(comment));

            return new Review(productId, userId, rating, comment);
        }

        public void ChangeRating(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating should be from 1 to 5", nameof(rating));
            Rating = rating;
            MarkAsUpdated();
        }

        public void ChangeComment(string? comment)
        {
            if (comment is not null && string.IsNullOrWhiteSpace(comment))
                throw new ArgumentException(
                    "Comment cannot be empty.",
                    nameof(comment));

            if (comment is not null && comment.Length > 1000)
                throw new ArgumentException(
                    "Comment cannot exceed 1000 characters.",
                    nameof(comment));

            Comment = comment;
            MarkAsUpdated();
        }
    }
}
