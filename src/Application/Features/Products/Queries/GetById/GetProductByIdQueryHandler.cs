using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetById;

internal class GetProductByIdQueryHandler(IAppDbContext context)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<GetProductByIdQueryResponse>> HandleAsync(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(p => p.CategoryProducts)
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
        {
            return Result.Failure<GetProductByIdQueryResponse>(ProductErrors.NotFound(request.Id));
        }

        var response = new GetProductByIdQueryResponse(
            product.Id,
            product.Name,
            product.Description,
            product.UnitsInStock,
            new(product.Price.Amount, product.Price.Currency),
            product.ImageURL,
            product.CategoryProducts.Select(cp => (cp.CategoryId, cp.CategoryName)).ToList()
        );

        return Result.Success(response);
    }
}