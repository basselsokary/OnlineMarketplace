using FluentValidation;

namespace Application.Features.Reviews.Queries.Get;

internal class GetReviewsForProductQueryValidator : AbstractValidator<GetReviewsForProductQuery>
{
    public GetReviewsForProductQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");
    }
}