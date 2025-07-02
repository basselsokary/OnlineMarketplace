namespace Application.Features.Reviews.Commands.Delete;

public record DeleteReviewCommand(Guid Id) : ICommand;
