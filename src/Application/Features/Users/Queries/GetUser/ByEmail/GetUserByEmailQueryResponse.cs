namespace Application.Features.Users.Queries.GetUser.ByEmail;

public record GetUserByEmailQueryResponse(
    string Id,
    string UserName,
    string Email,
    IEnumerable<string> Roles
);