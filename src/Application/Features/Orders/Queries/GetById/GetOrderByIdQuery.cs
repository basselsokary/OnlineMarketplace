namespace Application.Features.Orders.Queries.GetById;

public record GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdQueryResponse>;
