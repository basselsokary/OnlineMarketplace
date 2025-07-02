using Domain.Entities;
using Domain.Errors;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Commands.Create;

internal class CreateProductCommandHandler(IAppDbContext context)
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<Guid>> HandleAsync(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existingtCategories = await _context.Categories
            .Where(c => request.SelectedCategoryIds.Contains(c.Id))
            .Select(c => new { c.Id, c.Name })
            .ToListAsync(cancellationToken);

        bool allCategoriesExist = existingtCategories.Count == request.SelectedCategoryIds.Count;
        if (!allCategoriesExist)
            return Result.Failure<Guid>(CategoryErrors.InvalidCategories());
            
        var product = Product.Create(
            request.Name,
            request.Description,
            request.StockQuantity,
            Money.Create(request.Price.Amount, request.Price.Currency),
            request.ImageUrl,
            existingtCategories.Select(c => (c.Id, c.Name)).ToList());

        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Id);
    }
}