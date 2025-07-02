using Application.Common.Interfaces.Authentication;

namespace Application.Features.Users.Commands.Login;

internal class LoginCommandHandler(IIdentityService userService) : ICommandHandler<LoginCommand>
{
    private readonly IIdentityService _userService = userService;

    public async Task<Result> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.SignInAsync(request.Email, request.Password, request.RememberMe);
        if (!result.Succeeded)
            return result;

        return Result.Success();
    }
}
