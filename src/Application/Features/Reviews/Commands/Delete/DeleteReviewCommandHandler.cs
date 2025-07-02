using Application.Common.Interfaces.Authentication;

namespace Application.Features.Reviews.Commands.Delete;

internal class DeleteReviewCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteReviewCommand>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result> HandleAsync(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FindAsync([request.Id], cancellationToken);

        if (review is null)
        {
            return Result.Failure(["Review not found."]);
        }

        if (review.CustomerId.ToString() != _userContext.Id)
        {
            return Result.Failure(["You can only delete your own reviews."]);
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}