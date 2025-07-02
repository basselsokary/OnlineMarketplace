namespace Domain.Errors;

public static class ProductErrors
{
    public static IEnumerable<string> NotFound(Guid id)
        => [
        "Product.NotFound",
        $"Product {id} was not found."
    ];

    public static IEnumerable<string> InsufficientStock(Guid id, int unitsInStock)
        => [
        "Product.InsufficientStock",
        $"Product {id} has insufficient stock. Available units: {unitsInStock}."
    ];

    public static IEnumerable<string> CategoryAlreadyExists(Guid id)
        => [
        "Product.CategoryAlreadyExists",
        $"Category with ID {id} already exists in the product."
    ];

    public static IEnumerable<string> TooManyCategories(int maxCategoriesPerProduct)
        => [
        "Product.TooManyCategories",
        $"A product can have a maximum of {maxCategoriesPerProduct} categories."
    ];

    public static IEnumerable<string> NoCategoriesToClear(Guid id)
        => [
        "Product.NoCategoriesToClear",
        $"There are no categories to clear from the product {id}."
    ];

    public static IEnumerable<string> InvalidQuantity
        => [
        "Product.InvalidQuantity",
        "Quantity must be greater than zero."
    ];
}
