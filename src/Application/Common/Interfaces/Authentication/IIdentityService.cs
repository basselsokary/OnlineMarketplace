using Application.DTOs;
using Domain.Enums;

namespace Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<UserDto?> GetUserDtoByEmailAsync(string email);

    Task<UserDto?> GetUserDtoByIdAsync(string userId);

    Task<string?> GetRolesAsync(string userId);

    Task<Result> SignInAsync(string email, string password, bool rememberMe = false, bool lockoutOnFailure = false);
    Task<Result> SignOutAsync();

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string email, string password, UserRole role);
    
    Task<Result> DeleteUserAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> CheckPasswordAsync(string email, string password);

    Task<bool> IsUserExistAsync(string email);

    Task<bool> AddRoleToUserAsync(string userId, string role);
}
