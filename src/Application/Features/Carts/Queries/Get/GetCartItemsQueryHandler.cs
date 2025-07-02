using Application.Common.Interfaces.Authentication;
using Application.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Carts.Queries.Get;

internal class GetCartItemsQueryHandler(IAppDbContext context, IUserContext userContext)
    : IQueryHandler<GetCartItemsQuery, List<GetCartItemsQueryResponse>>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<List<GetCartItemsQueryResponse>>> HandleAsync(GetCartItemsQuery request, CancellationToken cancellationToken)
    {
        var cartId = await _context.Carts
            .Where(c => c.UserId == _userContext.Id)
            .Select(c => c.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (cartId == default)
            return Result.Success(new List<GetCartItemsQueryResponse>());

        var cartItems = await _context.CartItems
            .Where(ci => ci.CartId == cartId)
            .Select(ci => new
            {
                ci.Id,
                ci.ProductId,
                ci.Quantity
            }).ToListAsync(cancellationToken);

        if (!cartItems.Any())
            return Result.Success(new List<GetCartItemsQueryResponse>());
        
        var productIds = cartItems.Select(ci => ci.ProductId).ToList();

        var products = _context.Products.Where(p => productIds.Contains(p.Id))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.ImageURL
            });

        if (products.Count() != productIds.Count)
            return Result.Failure<List<GetCartItemsQueryResponse>>(["Some products were not found."]);

        var productMap = products.ToDictionary(p => p.Id);

        var response = cartItems.Select(ci =>
        {
            var product = productMap[ci.ProductId];
            return new GetCartItemsQueryResponse(
                ci.Id,
                ci.ProductId,
                ci.Quantity,
                product.Name,
                product.Description,
                product.ImageURL,
                product.Price.Map()
            );
        }).ToList();

        return Result.Success(response);
    }
}