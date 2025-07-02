using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Infrustructure;
using Infrastructure.Data.Context;
using Infrastructure.Email;
using Infrastructure.User;
using Infrastructure.User.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .AddServices()
                .AddDatabase(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IUserContext, CurrentUser>();

        services.AddTransient<IEmailSender, EmailSender>();

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddAuthorization();

        services.AddHttpContextAccessor();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        services.AddDbContext<AppIdentityDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("IdentitySqlServer")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 3;
        }).AddEntityFrameworkStores<AppIdentityDbContext>()
          .AddDefaultTokenProviders();

        return services;
    }
}
