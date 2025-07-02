using FluentValidation;
using static Domain.Constants.DomainConstants.Review;

namespace Application.Features.Reviews.Commands.Update;

internal class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Review ID is required.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(RatingMinValue, RatingMaxValue)
            .WithMessage($"Rating must be between {RatingMinValue} and {RatingMaxValue}.");

        RuleFor(x => x.Comment)
            .MaximumLength(CommentMaxLength)
            .WithMessage($"Comment cannot exceed {CommentMaxLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.Comment));
    }
}
