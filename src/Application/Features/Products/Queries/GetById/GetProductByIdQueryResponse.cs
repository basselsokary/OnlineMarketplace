using Application.DTOs;

namespace Application.Features.Products.Queries.GetById;

public record GetProductByIdQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    int UnitsInStock,
    MoneyDto Price,
    string? ImageUrl,
    IReadOnlyCollection<(Guid Id, string Name)> Categories
);