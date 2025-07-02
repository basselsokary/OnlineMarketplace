using System.Linq.Expressions;
using Application.DTOs;
using Domain.Entities;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetById;

internal class GetOrderByIdQueryHandler(IAppDbContext context)
    : IQueryHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<GetOrderByIdQueryResponse>> HandleAsync(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.Id == request.Id)
            .Select(GetProjection())
            .FirstOrDefaultAsync(cancellationToken);

        if (order == null)
        {
            return Result.Failure<GetOrderByIdQueryResponse>(OrderErrors.NotFound(request.Id));
        }

        return Result.Success(order);
    }

    private static Expression<Func<Order, GetOrderByIdQueryResponse>> GetProjection()
    {
        return order => new(
            order.Id,
            order.CreatedAt,
            order.Fulfilled,
            null,
            order.ShippedAt,
            order.Status,
            order.CustomerId,
            new(order.Address.Street, order.Address.District, order.Address.City, order.Address.ZipCode),
            new(order.TotalAmount.Amount, order.TotalAmount.Currency),
            order.OrderItems.Select(item => new OrderItemDto(
                item.Id,
                item.ProductId,
                item.Quantity,
                new(item.UnitPrice.Amount, item.UnitPrice.Currency)
            )).ToList()
        );
    }
}