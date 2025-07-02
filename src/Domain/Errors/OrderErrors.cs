namespace Domain.Errors;

public static class OrderErrors
{
    public static IEnumerable<string> NotFound(Guid id)
        => [
        "Order.NotFound",
        $"Order {id} not found."
    ];

    public static IEnumerable<string> AlreadyCancelled(Guid id)
        => [
        "Order.AlreadyCancelled",
        $"Order {id} is already cancelled."
    ];

    public static IEnumerable<string> AlreadyFulfilled(Guid id)
        => [
        "Order.AlreadyFulfilled",
        $"Order {id} is already fulfilled."
    ];

    public static IEnumerable<string> ItemAlreadyExists(Guid productId)
        => [
        "Order.ItemAlreadyExists",
        $"Order item of product {productId} already exists in the order."
    ];

    public static IEnumerable<string> InvalidQuantity(int quantity)
        => [
        "Order.InvalidQuantity",
        $"Invalid quantity: {quantity}. Quantity must be greater than zero."
    ];
}