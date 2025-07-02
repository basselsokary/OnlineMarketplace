using Application.Common.Validators;
using FluentValidation;
using static Domain.Constants.DomainConstants.Product;

namespace Application.Features.Products.Commands.Create;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(NameMaxLength)
            .WithMessage($"Product name cannot exceed {NameMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(DescriptionMaxLength)
            .WithMessage($"Description cannot exceed {DescriptionMaxLength} characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Price is required.")
            .SetValidator(new MoneyDtoValidator())
            .WithMessage("Invalid price format.");

        RuleFor(x => x.StockQuantity)
            .GreaterThan(0)
            .WithMessage("Stock quantity must be greater than zero.");

        RuleFor(x => x.SelectedCategoryIds)
            .NotEmpty()
            .WithMessage("At least one category must be selected.");

        RuleFor(x => x.ImageUrl)
            .MaximumLength(ImageUrlMaxLength)
            .WithMessage($"Image URL cannot exceed {ImageUrlMaxLength} characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.ImageUrl));
    }
}