namespace Domain.Errors;

public static class Errors
{
    public static IEnumerable<string> Unauthorized
        => [
        "Unauthorized",
        "You are not authorized to perform this action."
    ];
}
