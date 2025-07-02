using FluentValidation;
using static Domain.Constants.DomainConstants.Item;

namespace Application.Features.Carts.Commands.AddItem;

internal class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
{
    public AddCartItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(MaxItemQuantity)
            .WithMessage($"Quantity must be less than or equal {MaxItemQuantity}.");
    }
    
}