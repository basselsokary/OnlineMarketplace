namespace Application.Features.Products.Queries.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdQueryResponse>;
