using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Context;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager)
    {
        await SeedRolesAsync(roleManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = [nameof(UserRole.Admin), nameof(UserRole.Customer)];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}