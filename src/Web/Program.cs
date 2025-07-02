using Application;
using Infrastructure;
using Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace Web;

public class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddPresentation() // Add Presentation layer services
            .AddInfrastructure(builder.Configuration) // Add Infrastructure layer services
            .AddApplication(); // Add Application layer services


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // context.Database.EnsureCreated();
            // context.Database.EnsureDeleted();
            // context.Database.Migrate();

            await DatabaseSeeder.SeedAsync(roleManager);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseSession();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}