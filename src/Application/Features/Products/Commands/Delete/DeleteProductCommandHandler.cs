using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Delete;

internal class DeleteProductCommandHandler(IAppDbContext context)
    : ICommandHandler<DeleteProductCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            return Result.Failure(ProductErrors.NotFound(request.Id));

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
