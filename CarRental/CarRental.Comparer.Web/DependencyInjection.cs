﻿using CarRental.Comparer.Web.Extensions.DateExtensions;
using CarRental.Comparer.Web.Requests.CarServices;
using CarRental.Comparer.Web.Requests.OfferServices;
using CarRental.Comparer.Web.Requests.ProvidersServices;
using CarRental.Comparer.Web.Requests.RentalStatusServices;
using CarRental.Comparer.Web.Requests.RentalTransactionServices;
using CarRental.Comparer.Web.Requests.ReportServices;
using CarRental.Comparer.Web.Requests.UserServices;
using CarRental.Comparer.Web.RoleModels;
using CarRental.Comparer.Web.Services;
using CarRental.Comparer.Web.Services.GeolocationServices;
using CarRental.Comparer.Web.Services.StateContainer;
using CarRental.Comparer.Web.Settings;
using GoogleMapsComponents;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MudBlazor.Services;

namespace CarRental.Comparer.Web;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, AppSecrets configuration)
	{
		services.AddMsal(configuration);
		services.ConfigureHttpClients(configuration);

		services.AddMudServices();

		services.AddScoped<IGeoLocationService, GeoLocationService>();
		services.AddScoped<ThemeService>();
		services.AddScoped<StateContainer>();

		services.RegisterDateTimeServices();

		services.AddScoped<IRentalStatusService, RentalStatusService>();

		return services;
	}

	public static IServiceCollection RegisterDateTimeServices(this IServiceCollection services)
	{
		services.AddScoped<IDateTimeProvider, DateTimeProvider>();
		services.AddScoped<IYearsCalculator, YearsCalculator>();
		return services;
	}

	public static IServiceCollection AddMsal(this IServiceCollection services, AppSecrets configuration)
	{
		var scope = configuration.AzureAd?.ApiScope;

		ArgumentNullException.ThrowIfNull(scope, "Scope cannot be null");

		services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
		{
			options.ProviderOptions.Authentication.ClientId = configuration.AzureAd.ClientId;
			options.ProviderOptions.Authentication.Authority = configuration.AzureAd.Authority;	
			options.ProviderOptions.Authentication.ValidateAuthority = configuration.AzureAd.ValidateAuthority;	
			options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
			options.ProviderOptions.DefaultAccessTokenScopes.Add("email");
			options.UserOptions.RoleClaim = "appRole";
		}).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

		return services;
	}

	public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, AppSecrets configuration)
	{
		var baseUrl = configuration.ComparerApiSettings?.BaseUrl;

		ArgumentException.ThrowIfNullOrEmpty(baseUrl, "BaseUrl can not be null.");

		var apiScope = configuration.AzureAd?.ApiScope;

		ArgumentNullException.ThrowIfNull(apiScope, "Scope can not be null");

		services.AddHttpClient();

		services.AddHttpClient<ICarService, CarService>(client => client.BaseAddress = new Uri(baseUrl));

		services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(baseUrl))
			.AddHttpMessageHandler(sp => ConfigureAuthorizationHandler(sp, baseUrl, apiScope));

		services.AddHttpClient<IProviderService, ProviderService>(client => client.BaseAddress = new Uri(baseUrl));

		services.AddHttpClient<IRentalTransactionService, RentalTransactionService>(client => client.BaseAddress = new Uri(baseUrl))
			.AddHttpMessageHandler(sp => ConfigureAuthorizationHandler(sp, baseUrl, apiScope));
		
		services.AddHttpClient<IReportService, ReportService>(client => client.BaseAddress = new Uri(baseUrl))
			.AddHttpMessageHandler(sp => ConfigureAuthorizationHandler(sp, baseUrl, apiScope));

		services.AddHttpClient(OfferService.ClientWithoutAuthorization, client => client.BaseAddress = new Uri(baseUrl));
		
		services.AddHttpClient(OfferService.ClientWithAuthorization, client => client.BaseAddress = new Uri(baseUrl))
			.AddHttpMessageHandler(sp => ConfigureAuthorizationHandler(sp, baseUrl, apiScope));

		services.AddScoped<IOfferService, OfferService>();

		return services;
	}

	private static AuthorizationMessageHandler ConfigureAuthorizationHandler(IServiceProvider serviceProvider, string baseUrl, string apiScope)
	{
		var handler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>()
			.ConfigureHandler(
				authorizedUrls: [baseUrl],
				scopes: [apiScope]);
		return handler;
	}


	public static IServiceCollection ConfigureGoogleMaps(this IServiceCollection services, AppSecrets configuration)
	{
		var googleKey = configuration.GoogleMaps?.ApiKey;

		ArgumentException.ThrowIfNullOrEmpty(googleKey, "Api key for Google Maps can not be null or empty.");

		services.AddBlazorGoogleMaps(googleKey);

		return services;
	}
}