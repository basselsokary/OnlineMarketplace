using Application.Features.Orders.Commands.Fulfill;
using Domain.Errors;

namespace Application.Features.Orders.Commands.UpdateByAdmin;

internal class FulfillOrderCommandHandler(IAppDbContext context)
    : ICommandHandler<FulfillOrderCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result> HandleAsync(FulfillOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FindAsync([command.Id], cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(command.Id));
        }

        var result = order.Fulfill(command.OrderFulfilled);
        if (!result.Succeeded)
            return result;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
