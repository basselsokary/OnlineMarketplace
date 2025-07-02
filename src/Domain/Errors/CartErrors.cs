
namespace Domain.Errors;

public static class CartErrors
{
    public static IEnumerable<string> NotFound
        => [
        "Cart.NotFound",
        "Cart was not found."
    ];

    public static IEnumerable<string> ItemProductNotFound(Guid productId)
        => [
        "CartItem.NotFound",
        $"Cart Item of product {productId} was not found."
    ];

    public static IEnumerable<string> ItemNotFound(Guid itemId)
        => [
        "CartItem.NotFound",
        $"Cart Item {itemId} was not found."
    ];

    public static IEnumerable<string> AlreadyExists(Guid productId)
        => [
        "Cart.AlreadyExists",
        $"A cart item with product '{productId}' already exists."
    ];

    public static IEnumerable<string> NotValidQuantity(int quantity)
        => [
        "Invalid.Quantity",
        $"The provided quantity {quantity} is invalid."
    ];
}
