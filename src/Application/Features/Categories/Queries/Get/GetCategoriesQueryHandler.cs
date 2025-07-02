using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.Get;

internal class GetCategoriesQueryHandler(IAppDbContext context)
    : IQueryHandler<GetCategoriesQuery, List<GetCategoriesQueryResponse>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<List<GetCategoriesQueryResponse>>> HandleAsync(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .Select(GetProjection())
            .ToListAsync(cancellationToken);

        return Result.Success(categories);
    }

    private static Expression<Func<Category, GetCategoriesQueryResponse>> GetProjection()
    {
        return e => new(e.Id, e.Name, e.Description);
    }
}