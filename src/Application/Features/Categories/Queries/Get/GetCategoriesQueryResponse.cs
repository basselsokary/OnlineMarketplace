namespace Application.Features.Categories.Queries.Get;

public record GetCategoriesQueryResponse(
    Guid Id,
    string Name,
    string? Description
);
