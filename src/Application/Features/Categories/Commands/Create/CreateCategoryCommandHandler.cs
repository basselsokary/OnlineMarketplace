using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Commands.Create;

internal class CreateCategoryCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<Guid>> HandleAsync(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower(), cancellationToken))
        {
            return Result.Failure<Guid>(CategoryErrors.AlreadyExists(request.Name));
        }

        var category = Category.Create(request.Name, request.Description);

        category.UpdateCreatedBy(_userContext.Id);

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(category.Id);
    }
}
