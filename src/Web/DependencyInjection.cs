namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllersWithViews();

        // Cookies, Session, Cache
        services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true; // For GDPR compliance
        });

        return services;
    }

}
