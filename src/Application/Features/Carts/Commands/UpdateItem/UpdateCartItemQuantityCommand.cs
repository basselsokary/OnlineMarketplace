
namespace Application.Features.Carts.Commands.UpdateItem;

public record UpdateCartItemQuantityCommand(
    Guid ItemId,
    int Quantity) : ICommand;
