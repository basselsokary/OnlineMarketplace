using FluentValidation;
using static Domain.Constants.DomainConstants;

namespace Application.Features.Users.Queries.GetUser.ById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .MaximumLength(GuidIdMaxLength);
    }
}