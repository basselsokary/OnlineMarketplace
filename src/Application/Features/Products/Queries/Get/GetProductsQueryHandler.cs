using System.Linq.Expressions;
using Domain.Entities;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.Get;

internal class GetProductsQueryHandler(IAppDbContext context)
    : IQueryHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<List<GetProductsQueryResponse>>> HandleAsync(GetProductsQuery request, CancellationToken cancellationToken)
    {
        List<GetProductsQueryResponse> products;
        if (request.CategoryId != default)
        {
            var isCategoryExist = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!isCategoryExist)
            {
                return Result.Failure<List<GetProductsQueryResponse>>(CategoryErrors.NotFound(request.CategoryId));
            }
            
            products = await _context.Products
                .Include(p => p.CategoryProducts)
                .Where(p => p.CategoryProducts.Any(cp => cp.CategoryId == request.CategoryId))
                .Select(GetProjection())
                .ToListAsync(cancellationToken);

            return Result.Success(products);
        }

        products = await _context.Products
            .Include(p => p.CategoryProducts)
            .Select(GetProjection())
            .ToListAsync(cancellationToken);

        return Result.Success(products);
    }

    private Expression<Func<Product, GetProductsQueryResponse>> GetProjection()
    {
        return product => new GetProductsQueryResponse(
            product.Id,
            product.Name,
            product.Description,
            product.UnitsInStock,
            new(product.Price.Amount, product.Price.Currency),
            product.ImageURL,
            product.CategoryProducts.Select(cp => cp.CategoryName).ToList()
        );
    }
}