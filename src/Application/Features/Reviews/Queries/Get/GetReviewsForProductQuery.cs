namespace Application.Features.Reviews.Queries.Get;

public record GetReviewsForProductQuery(Guid ProductId) : IQuery<List<GetReviewsForProductQueryResponse>>;
