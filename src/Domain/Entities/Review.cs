using Domain.Common;
using Domain.Constants;
using static Domain.Constants.DomainConstants.Review;

namespace Domain.Entities;

public class Review : BaseAuditableEntity<Guid>
{
    private Review() : base(Guid.Empty) { }
    private Review(int rating, string? comment, Guid customerId, Guid productId)
        : base(Guid.NewGuid())
    {
        Rating = rating;
        Comment = comment;
        CustomerId = customerId;
        ProductId = productId;
    }

    public int Rating { get; private set; }

    public string? Comment { get; private set; }

    public Guid CustomerId { get; private set; }

    public Guid ProductId { get; private set; }

    public static Review Create(int rating, string? comment, Guid customerId, Guid productId)
    {
        return new Review(rating, comment, customerId, productId);
    }

    public void Update(string? comment, int rating)
    {
        if (rating < RatingMinValue || rating > RatingMaxValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(rating),
                $"Rating must be between {RatingMinValue} and {RatingMaxValue}.");
        }

        Comment = comment;
        Rating = rating;
    }
}