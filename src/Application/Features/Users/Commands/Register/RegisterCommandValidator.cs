using FluentValidation;
using static Domain.Constants.DomainConstants.User;

namespace Application.Features.Users.Commands.Register;

internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(UserNameMaxLength)
            .WithMessage($"Username must not exceed {UserNameMaxLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(UserNameMaxLength)
            .WithMessage($"Email must not exceed {UserNameMaxLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(PasswordMinLength)
            .WithMessage($"Password must be at least {PasswordMinLength} characters long.")
            .MaximumLength(PasswordMaxLength)
            .WithMessage($"Password must not exceed {PasswordMaxLength} characters.");
    }
}