using Application.Common.Interfaces.Authentication;
using Application.Mappers;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Queries.Profile;

public record GetProfileQuery : IQuery<GetProfileQueryResponse>;

internal class GetProfileQueryHandler(IAppDbContext context, IUserContext userContext)
    : IQueryHandler<GetProfileQuery, GetProfileQueryResponse>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<GetProfileQueryResponse>> HandleAsync(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var profileResponse = await _context.Customers
            .Where(c => c.UserId == _userContext.Id)
            .Select(c => new GetProfileQueryResponse(
                c.FullName,
                c.Age,
                new(
                    c.Address.Street,
                    c.Address.District,
                    c.Address.City,
                    c.Address.ZipCode
                    ),
                c.CreatedAt)
            ).FirstOrDefaultAsync(cancellationToken);

        if (profileResponse == null)
            return Result.Failure<GetProfileQueryResponse>(CustomerErrors.NotFound);

        return Result.Success(profileResponse);
    }
}