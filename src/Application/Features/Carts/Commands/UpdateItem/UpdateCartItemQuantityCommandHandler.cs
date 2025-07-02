using Application.Common.Interfaces.Authentication;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Carts.Commands.UpdateItem;

internal class UpdateCartItemQuantityCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<UpdateCartItemQuantityCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == _userContext.Id, cancellationToken: cancellationToken);

        if (cart == null)
            return Result.Failure(CartErrors.NotFound);

        var result = cart.UpdateItem(request.ItemId, request.Quantity);
        if (!result.Succeeded)
            return result;

        return Result.Success();
    }
}