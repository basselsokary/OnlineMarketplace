using System.Linq.Expressions;
using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.Get.GetForCustomer;

internal class GetForCustomerQueryHandler(IAppDbContext context, IUserContext userContext)
    : IQueryHandler<GetForCustomerQuery, List<GetForCustomerQueryResponse>>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<List<GetForCustomerQueryResponse>>> HandleAsync(
        GetForCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _context.Orders
            .Where(o => o.UserId == _userContext.Id)
            .Select(GetProjection())
            .ToListAsync(cancellationToken);

        return Result.Success(orders);
    }

    private static Expression<Func<Order, GetForCustomerQueryResponse>> GetProjection()
    {
        return order => new GetForCustomerQueryResponse(
            order.Id,
            order.Status,
            new(order.TotalAmount.Amount, order.TotalAmount.Currency),
            order.CreatedAt
        );
    }
}