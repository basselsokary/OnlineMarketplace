using Application.Common.Interfaces.Authentication;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Commands.Cancel;

internal class CancelOrderCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<CancelOrderCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == request.Id && o.UserId == _userContext.Id, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.Id));
        }

        var result = order.Cancel();
        if (!result.Succeeded)
            return result;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}