using Domain.Errors;

namespace Application.Features.Categories.Commands.Delete;

internal class DeleteCategoryCommandHandler(IAppDbContext context)
    : ICommandHandler<DeleteCategoryCommand>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result> HandleAsync(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync([request.Id], cancellationToken);
        if (category == null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.Id));
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}