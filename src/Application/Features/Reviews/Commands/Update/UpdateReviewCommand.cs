namespace Application.Features.Reviews.Commands.Update;

public record UpdateReviewCommand(
    Guid Id,
    string Comment,
    int Rating
) : ICommand;
