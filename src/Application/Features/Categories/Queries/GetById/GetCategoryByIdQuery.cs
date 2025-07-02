namespace Application.Features.Categories.Queries.GetById;

public record GetCategoryByIdQuery(Guid Id) : IQuery<GetCategoryByIdQueryResponse>;
