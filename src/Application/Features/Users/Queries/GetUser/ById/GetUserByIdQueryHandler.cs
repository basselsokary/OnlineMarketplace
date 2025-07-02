using Application.Common.Interfaces.Authentication;
using Application.DTOs;
using Domain.Errors;

namespace Application.Features.Users.Queries.GetUser.ById;

internal class GetUserByIdQueryHandler(IIdentityService userService)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    private readonly IIdentityService _userService = userService;

    public async Task<Result<GetUserByIdQueryResponse>> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserDtoByIdAsync(request.UserId);

        if (user == null)
            return Result.Failure<GetUserByIdQueryResponse>(UserErrors.NotFound);

        return Result.Success(ToResponse(user));
    }
    
    private static GetUserByIdQueryResponse? ToResponse(UserDto user)
    {
        return new GetUserByIdQueryResponse(user.Id, user.UserName, user.Email, user.Roles);
    }
}
