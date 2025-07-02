namespace Domain.Errors;

public static class ReviewErrors
{
    public static IEnumerable<string> NotFound(Guid id)
        => [
        "Review.NotFound",
        $"Review {id} not found."
    ];

    public static IEnumerable<string> AlreadyExists(Guid productId)
        => [
        "Review.AlreadyExists",
        $"You have already reviewed this product {productId}."
    ];
}
