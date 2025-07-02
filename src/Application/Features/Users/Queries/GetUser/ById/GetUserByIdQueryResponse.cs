namespace Application.Features.Users.Queries.GetUser.ById;

public record GetUserByIdQueryResponse(
    string Id,
    string UserName,
    string Email,
    IEnumerable<string> Roles);
