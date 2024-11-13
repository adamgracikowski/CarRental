using CarRental.Comparer.Web.Requests.CarServices;
using CarRental.Comparer.Web.Requests.UserServices;
using CarRental.Comparer.Web.Services;
using CarRental.Comparer.Web.Services.StateContainer;
using Darnton.Blazor.DeviceInterop.Geolocation;
using MudBlazor.Services;

namespace CarRental.Comparer.Web;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOidcAuthentication(configuration);
		services.ConfigureHttpClients(configuration);

		services.AddMudServices();

		services.AddScoped<IGeolocationService, GeolocationService>();
		services.AddScoped<ThemeService>();
		services.AddScoped<StateContainer>();

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

    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
		var baseUrl = configuration.GetValue<string>("ComparerApiSettings:BaseUrl");

        ArgumentException.ThrowIfNullOrEmpty(baseUrl, "BaseUrl can not be null.");

        services.AddHttpClient<ICarService, CarService>(client => client.BaseAddress = new Uri(baseUrl));
        services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(baseUrl));

        return services;
    }
}