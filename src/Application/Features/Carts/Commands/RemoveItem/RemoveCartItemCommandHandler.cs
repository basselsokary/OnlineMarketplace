using Application.Common.Interfaces.Authentication;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Carts.Commands.RemoveItem;

internal class RemoveCartItemCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<RemoveCartItemCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == _userContext.Id, cancellationToken);
        if (cart == null)
            return Result.Failure(CartErrors.NotFound);

        var result = cart.RemoveItem(request.ItemId);
        if (!result.Succeeded)
            return result;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
