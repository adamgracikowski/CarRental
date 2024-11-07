using CarRental.Comparer.Web.Requests.Users.UserApiService;
using CarRental.Comparer.Web.Services;
using Darnton.Blazor.DeviceInterop.Geolocation;
using MudBlazor.Services;

namespace CarRental.Comparer.Web;

public static class DependencyInjection
{
    public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOidcAuthentication(configuration);
        services.SetHttpClientBaseUrl(configuration);

        return services;
    }

    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddMudServices();
        services.AddScoped<GeolocationService>();
        services.AddScoped<UserApiService>();
        services.AddScoped<ThemeService>();
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7016") });

        return services;
    }

    public static IServiceCollection AddOidcAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOidcAuthentication(options =>
        {
            configuration.Bind("Auth", options.ProviderOptions);
            options.ProviderOptions.DefaultScopes.Add("email");
        });

        return services;
    }

    public static IServiceCollection SetHttpClientBaseUrl(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration["ApiSettings:BaseAddress"];
        if (string.IsNullOrEmpty(baseAddress))
        {
            throw new InvalidOperationException("BaseAddress configuration is missing.");
        }

        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

        return services;
    }
}