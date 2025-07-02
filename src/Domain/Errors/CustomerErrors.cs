namespace Domain.Errors;

public static class CustomerErrors
{
    public static IEnumerable<string> NotFound
        => [
        "Customer.NotFound",
        $"Customer was not found."
    ];
}
