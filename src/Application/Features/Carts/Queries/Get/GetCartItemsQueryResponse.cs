using Application.DTOs;

namespace Application.Features.Carts.Queries.Get;

public record GetCartItemsQueryResponse(
    Guid Id,
    Guid ProductId,
    int Quantity,
    string ProductName,
    string? ProductDescription,
    string? ProductImageUrl,
    MoneyDto ProductPrice
);
