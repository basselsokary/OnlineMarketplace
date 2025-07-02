using FluentValidation;

namespace Application.Features.Carts.Commands.RemoveItem;

internal class RemoveCartItemCommandValidator : AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemCommandValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("Item ID is required.");
    }
    
}