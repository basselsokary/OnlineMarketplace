using Application.Common.Interfaces.Authentication;
using Domain.Errors;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Customers.Commands.Update;

internal class UpdateCustomerCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == _userContext.Id, cancellationToken);
        if (customer == null)
            return Result.Failure(CustomerErrors.NotFound);

        var address = Address.Create(
            request.Address.Street,
            request.Address.District,
            request.Address.City,
            request.Address.ZipCode);

        var result = customer.Update(request.FullName, address, request.Age);
        if (!result.Succeeded)
            return result;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}