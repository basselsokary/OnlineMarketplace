using FluentValidation;
using static Domain.Constants.DomainConstants.Review;

namespace Application.Features.Reviews.Commands.Create;

internal class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(RatingMinValue, RatingMaxValue)
            .WithMessage($"Rating must be between {RatingMinValue} and {RatingMaxValue}.");

        RuleFor(x => x.Comment)
            .MaximumLength(CommentMaxLength)
            .WithMessage($"Comment cannot exceed {CommentMaxLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.Comment));
    }
}