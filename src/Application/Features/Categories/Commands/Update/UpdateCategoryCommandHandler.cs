using Application.Common.Interfaces.Authentication;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Commands.Update;

internal class UpdateCategoryCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<UpdateCategoryCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        if (category == null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.Id));
        }

        if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower() && c.Id != request.Id, cancellationToken))
        {
            return Result.Failure(CategoryErrors.AlreadyExists(request.Name));
        }

        category.Update(request.Name, request.Description);
        category.UpdateLastModifiedBy(_userContext.Id);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}