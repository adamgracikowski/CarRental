using CarRental.Comparer.Infrastructure.CarProviders.Authorization;
using CarRental.Comparer.Infrastructure.CarProviders.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Comparer.Infrastructure.HttpClients;

public static class HttpClientsConfiguration
{
	public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		var carProviderOptions = configuration.GetSection(CarProvidersOptions.SectionName).Get<CarProvidersOptions>();

		ArgumentNullException.ThrowIfNull(carProviderOptions, $"{CarProvidersOptions.SectionName} can not be null.");

		services.AddHttpClient();

		services.AddHttpClient(carProviderOptions.ExternalProvider.Name, client =>
		{
			client.BaseAddress = new Uri(carProviderOptions.ExternalProvider.BaseUrl);
			client.DefaultRequestHeaders.Add(carProviderOptions.ExternalProvider.HttpKeySectionName, carProviderOptions.ExternalProvider.ApiKey);
		});

		services.AddHttpClient(carProviderOptions.InternalProvider.Name, client =>
		{
			client.BaseAddress = new Uri(carProviderOptions.InternalProvider.BaseUrl);
		}).AddHttpMessageHandler<TokenAuthorizationHandler>();

		return services;
	}
}