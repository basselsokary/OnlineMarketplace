using Application.Common.Interfaces.Authentication;
using Domain.Errors;

namespace Application.Features.Reviews.Commands.Update;

internal class UpdateReviewCommandHandler(IUserContext userContext, IAppDbContext context)
    : ICommandHandler<UpdateReviewCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FindAsync([request.Id], cancellationToken);
        if (review == null)
        {
            return Result.Failure(ReviewErrors.NotFound(request.Id));
        }

        if (review.CustomerId.ToString() != _userContext.Id)
        {
            return Result.Failure(Errors.Unauthorized);
        }

        review.Update(request.Comment, request.Rating);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}