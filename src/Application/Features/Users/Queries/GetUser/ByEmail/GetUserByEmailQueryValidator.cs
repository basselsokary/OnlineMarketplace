using FluentValidation;
using static Domain.Constants.DomainConstants.User;

namespace Application.Features.Users.Queries.GetUser.ByEmail;
public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(EmailMaxLength)
            .WithMessage($"Email must not exceed {EmailMaxLength} characters.");
    }
}