
namespace Application.Features.Carts.Commands.RemoveItem;

public record RemoveCartItemCommand(Guid ItemId) : ICommand;
