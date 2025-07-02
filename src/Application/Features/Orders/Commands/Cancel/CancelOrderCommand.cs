namespace Application.Features.Orders.Commands.Cancel;

public record CancelOrderCommand(
    Guid Id
) : ICommand;
