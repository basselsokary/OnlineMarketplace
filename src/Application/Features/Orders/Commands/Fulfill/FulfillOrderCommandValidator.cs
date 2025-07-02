using Application.Features.Orders.Commands.Fulfill;
using FluentValidation;

namespace Application.Features.Orders.Commands.UpdateByAdmin;

internal class FulfillOrderCommandValidator : AbstractValidator<FulfillOrderCommand>
{
    public FulfillOrderCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID is required.");

        RuleFor(x => x.OrderFulfilled)
            .NotEmpty()
            .WithMessage("Order fulfilled date is required.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid order status.");
    }
}