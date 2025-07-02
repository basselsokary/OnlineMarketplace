using Application.DTOs;
using Domain.Enums;

namespace Application.Features.Orders.Queries.GetById;

public record GetOrderByIdQueryResponse(
    Guid Id,
    DateTime CreatedAt,
    DateTime? OrderFulfilled,
    DateTime? PaidAt,
    DateTime? ShippedAt,
    OrderStatus Status,
    Guid CustomerId,
    AddressDto Address,
    MoneyDto TotalAmount,
    IReadOnlyCollection<OrderItemDto> OrderItems
);
