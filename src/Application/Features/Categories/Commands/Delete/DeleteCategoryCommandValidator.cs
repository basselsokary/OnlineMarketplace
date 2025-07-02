using FluentValidation;

namespace Application.Features.Categories.Commands.Delete;

internal class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category Id is required.");
    }
}