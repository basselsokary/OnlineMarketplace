namespace Application.DTOs;

public record OrderItemDto(
    Guid Id,
    Guid ProductId,
    int Quantity,
    MoneyDto UnitPrice
);
