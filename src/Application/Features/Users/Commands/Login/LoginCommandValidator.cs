using FluentValidation;
using static Domain.Constants.DomainConstants.User;

namespace Application.Features.Users.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(EmailMaxLength)
            .WithMessage($"Email must not exceed {EmailMaxLength} characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(PasswordMinLength)
            .WithMessage($"Password must be at least {PasswordMinLength} characters long.")
            .MaximumLength(PasswordMaxLength)
            .WithMessage($"Password must not exceed {PasswordMaxLength} characters.");
    }
}