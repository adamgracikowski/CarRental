using CarRental.Comparer.Web.Requests.AvailableCars.AvailableCarsService;
using CarRental.Comparer.Web.Requests.Users.UserApiService;
using CarRental.Comparer.Web.Services;
using CarRental.Comparer.Web.Services.MakeLogoService;
using CarRental.Comparer.Web.Services.StateContainer;
using Darnton.Blazor.DeviceInterop.Geolocation;
using MudBlazor.Services;

namespace CarRental.Comparer.Web;

public static class DependencyInjection
{
	public static IServiceCollection RegisterConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOidcAuthentication(configuration);
		services.AddComparerHttpClient(configuration);
		services.AddProviderHttpClient(configuration);
		services.AddMakeLogoService(configuration);

		return services;
	}

	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
	{
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

	public static IServiceCollection AddMakeLogoService(this IServiceCollection services, IConfiguration configuration)
	{
		var baseAddress = configuration["ComparerApiSettings:BaseAddress"];
		if (string.IsNullOrEmpty(baseAddress))
		{
			throw new InvalidOperationException("BaseAddress configuration is missing.");
		}
		services.AddHttpClient<IMakeLogoService, MakeLogoService>(client => client.BaseAddress = new Uri(baseAddress));

		return services;
	}

	public static IServiceCollection AddComparerHttpClient(this IServiceCollection services, IConfiguration configuration)
	{
		var baseAddress = configuration["ComparerApiSettings:BaseAddress"];
		if (string.IsNullOrEmpty(baseAddress))
		{
			throw new InvalidOperationException("BaseAddress configuration is missing.");
		}
		services.AddHttpClient<IUserApiService, UserApiService>(client => client.BaseAddress = new Uri(baseAddress));

		return services;
	}

	public static IServiceCollection AddProviderHttpClient(this IServiceCollection services, IConfiguration configuration)
	{
		var baseAddress = configuration["ProviderApiSettings:BaseAddress"];
		if (string.IsNullOrEmpty(baseAddress))
		{
			throw new InvalidOperationException("BaseAddress configuration is missing.");
		}
		services.AddHttpClient<IAvailableCarsService, AvailableCarsService>(client => client.BaseAddress = new Uri(baseAddress));

		return services;
	}
}