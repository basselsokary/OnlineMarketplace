using Application.DTOs;

namespace Application.Features.Products.Queries.Get;

public record GetProductsQueryResponse(
    Guid Id,
    string Name,
    string? Description,
    int UnitsInStock,
    MoneyDto Price,
    string? ImageUrl,
    IReadOnlyCollection<string> CategoryNames);