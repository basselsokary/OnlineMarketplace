using Application.Common.Interfaces.Authentication;

namespace Application.Features.Users.Commands.Logout;

internal class LogoutCommandHandler(IIdentityService identityService) : ICommandHandler<LogoutCommand>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result> HandleAsync(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.SignOutAsync();
        return Result.Success();
    }
}
