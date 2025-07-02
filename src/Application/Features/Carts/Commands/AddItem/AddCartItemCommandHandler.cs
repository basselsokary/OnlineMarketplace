using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Domain.Errors;

namespace Application.Features.Carts.Commands.AddItem;

internal class AddCartItemCommandHandler(IAppDbContext context, IUserContext userContext) : ICommandHandler<AddCartItemCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = _context.Carts.FirstOrDefault(c => c.UserId == _userContext.Id);

        // If cart doesn't exist, create one
        if (cart == null)
        {
            var customerId = _context.Customers
                .Where(c => c.UserId == _userContext.Id)
                .Select(c => c.Id)
                .FirstOrDefault();

            if (customerId == default)
                return Result.Failure(CustomerErrors.NotFound);

            cart = Cart.Create(customerId, _userContext.Id);
            await _context.Carts.AddAsync(cart, cancellationToken);
        }

        var result = cart.AddItem(request.ProductId, request.Quantity);
        if (!result.Succeeded)
            return result;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}