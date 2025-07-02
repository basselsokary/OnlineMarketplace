namespace Domain.Errors;

public static class UserErrors
{
    public static IEnumerable<string> NotFound
        => [
        "User.NotFound",
        $"User was not found."
    ];

    public static IEnumerable<string> InvalidCredentials
        => [
        "User.InvalidCredentials",
        $"Invalid credentials."
    ];

    public static IEnumerable<string> EmailAlreadyExists
        => [
        "User.EmailAlreadyExists",
        $"Email already exists."
    ];
}