using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.Get;

internal class GetReviewsForProductQueryHandler(IAppDbContext context)
    : IQueryHandler<GetReviewsForProductQuery, List<GetReviewsForProductQueryResponse>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<List<GetReviewsForProductQueryResponse>>> HandleAsync(GetReviewsForProductQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _context.Reviews
            .Where(r => r.ProductId == request.ProductId)
            .Select(GetProjection())
            .ToListAsync(cancellationToken);

        return Result.Success(reviews);
    }

    private static Expression<Func<Review, GetReviewsForProductQueryResponse>> GetProjection()
    {
        return review => new GetReviewsForProductQueryResponse(
            review.Id,
            review.ProductId,
            review.CustomerId,
            review.Comment ?? string.Empty,
            review.Rating,
            review.CreatedAt,
            review.LastModifiedAt);
    }
}