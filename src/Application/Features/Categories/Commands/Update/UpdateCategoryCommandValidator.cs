using FluentValidation;
using static Domain.Constants.DomainConstants.Category;

namespace Application.Features.Categories.Commands.Update;

internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(NameMaxLength)
            .WithMessage($"Name must not exceed {NameMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(DescriptionMaxLength)
            .WithMessage($"Description must not exceed {DescriptionMaxLength} characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}