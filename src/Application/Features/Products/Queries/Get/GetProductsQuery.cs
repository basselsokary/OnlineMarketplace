namespace Application.Features.Products.Queries.Get;

public record GetProductsQuery(Guid CategoryId) : IQuery<List<GetProductsQueryResponse>>;
