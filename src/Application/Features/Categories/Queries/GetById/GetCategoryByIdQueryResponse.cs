namespace Application.Features.Categories.Queries.GetById;

public record GetCategoryByIdQueryResponse(
    Guid Id,
    string Name,
    string? Description
);