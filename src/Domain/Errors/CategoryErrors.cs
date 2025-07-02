namespace Domain.Errors;

public static class CategoryErrors
{
    public static IEnumerable<string> NotFound(Guid id)
        => [
        "Category.NotFound",
        $"Category {id} was not found."
    ];

    public static IEnumerable<string> AlreadyExists(string name)
        => [
        "Category.AlreadyExists",
        $"A category with the same name '{name}' already exists."
    ];

    public static IEnumerable<string> InvalidCategories()
        => [
        "Category.InvalidCategories",
        $"One or more categories are invalid or do not exist."
    ];
}
