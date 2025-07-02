namespace Application.Features.Categories.Commands.Delete;

public record DeleteCategoryCommand(
    Guid Id
) : ICommand;
