using Domain.Enums;

namespace Application.Features.Orders.Commands.Fulfill;

public record FulfillOrderCommand(
    Guid Id,
    DateTime OrderFulfilled,
    OrderStatus Status
) : ICommand;
