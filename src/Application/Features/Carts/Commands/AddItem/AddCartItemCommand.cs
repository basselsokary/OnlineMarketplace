namespace Application.Features.Carts.Commands.AddItem;

public record AddCartItemCommand(Guid ProductId, int Quantity) : ICommand;
