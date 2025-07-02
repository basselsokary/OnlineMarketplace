namespace Application.Features.Products.Commands.Delete;

public record DeleteProductCommand(Guid Id) : ICommand;
