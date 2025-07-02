namespace Application.Features.Reviews.Queries.Get;

public record GetReviewsForProductQueryResponse(
    Guid Id,
    Guid ProductId,
    Guid CustomerId,
    string Comment,
    int Rating,
    DateTime CreatedAt,
    DateTime UpdatedAt
);