using Application.Common.Interfaces.Authentication;
using Domain.Entities;
using Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Commands.Create;

internal class CreateReviewCommandHandler(IAppDbContext context, IUserContext userContext)
    : ICommandHandler<CreateReviewCommand, string>
{
    private readonly IAppDbContext _context = context;
    private readonly IUserContext _userContext = userContext;

    public async Task<Result<string>> HandleAsync(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        // Check if the user already has a review for the product
        var hasReview = await _context.Reviews
            .AnyAsync(r => r.ProductId == request.ProductId && r.CustomerId.ToString() == _userContext.Id, cancellationToken);

        if (hasReview)
        {
            return Result.Failure<string>(ReviewErrors.AlreadyExists(request.ProductId));
        }

        var review = Review.Create(
            request.Rating,
            request.Comment,
            Guid.Parse(_userContext.Id),
            request.ProductId
        );

        await _context.Reviews.AddAsync(review, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(review.Id.ToString());
    }
}