using Domain.Errors;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Update;

internal class UpdateProductCommandHandler(IAppDbContext context)
    : ICommandHandler<UpdateProductCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result> HandleAsync(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product == null)
            return Result.Failure(ProductErrors.NotFound(request.Id));

        var existingtCategories = await _context.Categories
            .Where(c => request.CategoryIds.Contains(c.Id))
            .Select(c => new { c.Id, c.Name })
            .ToListAsync(cancellationToken);

        bool allCategoriesExist = existingtCategories.Count == request.CategoryIds.Count;
        if (allCategoriesExist)
            return Result.Failure<Guid>(CategoryErrors.InvalidCategories());

        product.ClearCategories();
        product.Update(
            request.Name,
            request.Description,
            request.StockQuantity,
            Money.Create(request.Price.Amount, request.Price.Currency),
            request.ImageUrl,
            existingtCategories.Select(c => (c.Id, c.Name)).ToList()
            );

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}