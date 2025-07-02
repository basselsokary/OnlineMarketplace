namespace Application.Features.Categories.Commands.Create;

public record CreateCategoryCommand(
    string Name,
    string? Description
) : ICommand<Guid>;
