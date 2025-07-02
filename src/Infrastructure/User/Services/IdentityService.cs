using Application.Common.Interfaces.Authentication;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Models;
using Domain.Enums;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Domain.Errors;

namespace Infrastructure.User.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<UserDto?> GetUserDtoByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user == null ? null
            : await ToUserDto(user);
    }

    public async Task<UserDto?> GetUserDtoByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user == null ? null
            : await ToUserDto(user);
    }

    public async Task<string?> GetRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return null;

        return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
    }

    private async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        => await _userManager.GetRolesAsync(user);

    public async Task<Result> SignInAsync(string email, string password, bool rememberMe = false, bool lockoutOnFailure = false)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Failure(UserErrors.NotFound);

        var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure);
        return result.ToApplicationResult();
    }

    public async Task<Result> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return Result.Success();
    }
    
    public async Task<(Result Result, string UserId)> CreateUserAsync(
        string userName,
        string email,
        string password,
        UserRole role)
    {
        ApplicationUser user = role switch
        {
            UserRole.Admin => new Admin(),
            UserRole.Customer => new AppUser(),
            _ => throw new ArgumentException($"Invalid Role")
        };

        user.Email = email;
        user.UserName = userName;

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Failed to create user.");
        }

        result = await _userManager.AddToRoleAsync(user, role.GetName());

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return false;

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user != null && await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> AddRoleToUserAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult.Succeeded;
    }

    private async Task<UserDto> ToUserDto(ApplicationUser user)
    {
        return new(
            user.Id,
            user.Email!,
            user.UserName!,
            await GetRolesAsync(user));
    }
}
