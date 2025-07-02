namespace Application.Features.Users.Queries.GetUser.ByEmail;

public record GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailQueryResponse>;
