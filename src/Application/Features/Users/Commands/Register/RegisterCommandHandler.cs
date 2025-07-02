using Application.Common.Interfaces.Authentication;
using Domain.Enums;
using Domain.Errors;

namespace Application.Features.Users.Commands.Register;

internal class RegisterCommandHandler(IIdentityService userService) : ICommandHandler<RegisterCommand>
{
    private readonly IIdentityService _userService = userService;

    public async Task<Result> HandleAsync(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _userService.IsUserExistAsync(request.Email);
        if (userExist)
            return Result.Failure(UserErrors.EmailAlreadyExists);

        var result = await _userService.CreateUserAsync(
            request.UserName,
            request.Email,
            request.Password,
            UserRole.Customer); // Default role is Customer
            
        if (!result.Result.Succeeded)
            return result.Result;

        return Result.Success();
    }
}
