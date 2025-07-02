namespace Application.Features.Categories.Commands.Update;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description
) : ICommand;
