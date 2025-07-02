using Application.DTOs;
using Domain.Enums;

namespace Application.Features.Orders.Queries.Get.GetForCustomer;

public record GetForCustomerQueryResponse(
    Guid Id,
    OrderStatus Status,
    MoneyDto TotalAmount,
    DateTime CreatedAt
);