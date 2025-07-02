using System.Linq.Expressions;
using Domain.Entities;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetById;

internal class GetCategoryByIdQueryHandler(IAppDbContext context)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<GetCategoryByIdQueryResponse>> HandleAsync(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Where(c => c.Id == request.Id)
            .Select(GetProjection())
            .FirstOrDefaultAsync(cancellationToken);

        if (category == null)
        {
            return Result.Failure<GetCategoryByIdQueryResponse>(CategoryErrors.NotFound(request.Id));
        }

        return Result.Success(category);
    }

    private Expression<Func<Category, GetCategoryByIdQueryResponse>> GetProjection()
    {
        return e => new(e.Id, e.Name, e.Description);
    }
}