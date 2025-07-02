using FluentValidation;

namespace Application.Features.Reviews.Commands.Delete;

internal class DeleteReviewCommandValidator : AbstractValidator<DeleteReviewCommand>
{
    public DeleteReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Review ID is required.");
    }
}
