using Application.Common.Validators;
using FluentValidation;
using static Domain.Constants.DomainConstants.Customer;

namespace Application.Features.Customers.Commands.Update;

internal class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(FullNameMaxLength)
            .WithMessage($"Full name cannot exceed {FullNameMaxLength} characters.");

        RuleFor(c => c.Address)
            .SetValidator(new AddressDtoValidator())
            .WithMessage("Address is required.");

        RuleFor(c => c.Age)
            .GreaterThanOrEqualTo(AgeMin)
            .WithMessage($"Age must be at least {AgeMin} years old.");
    }
}