using FluentValidation;

namespace Application.Features.Orders.Commands.Cancel;

internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required.");
    }
}