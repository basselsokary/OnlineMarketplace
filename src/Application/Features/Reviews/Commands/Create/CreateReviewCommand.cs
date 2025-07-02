namespace Application.Features.Reviews.Commands.Create;

public record CreateReviewCommand(
    Guid ProductId,
    int Rating,
    string? Comment) : ICommand<string>;
