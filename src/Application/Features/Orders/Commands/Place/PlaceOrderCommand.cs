using Application.DTOs;
using Domain.Enums;

namespace Application.Features.Orders.Commands.Place;

public record PlaceOrderCommand(
    List<OrderItemDto> OrderItems,
    AddressDto Address,
    PaymentMethod PaymentMethod
) : ICommand<Guid>;




