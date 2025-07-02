using Domain.Enums;

namespace Application.Helpers;

public static class UserRoleExtensions
{
    public static string GetName(this UserRole role) => role switch
    {
        UserRole.Admin => nameof(UserRole.Admin),
        UserRole.Customer => nameof(UserRole.Customer),
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };

    public static UserRole GetRole(this string roleString) => roleString switch
    {
        nameof(UserRole.Admin) => UserRole.Admin,
        nameof(UserRole.Customer) => UserRole.Customer,
        _ => throw new ArgumentOutOfRangeException(roleString, roleString, null)
    };
}
