using CarRental.Comparer.Web;
using CarRental.Comparer.Web.Settings.ConfigurationServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddGeolocationServices();

builder.Services.AddScoped(sp =>
{
	var hostEnvironment = builder.HostEnvironment.Environment;
	var baseUrl = builder.Configuration[$"{hostEnvironment}:ApiBaseUrl"];

	ArgumentNullException.ThrowIfNull(baseUrl, nameof(baseUrl));

	return new HttpClient { BaseAddress = new Uri(baseUrl) };
});

builder.Services.AddScoped(sp => new ConfigurationService(sp.GetRequiredService<HttpClient>()));

var configurationService = builder.Services.BuildServiceProvider().GetRequiredService<ConfigurationService>();
var configurationResponse = await configurationService.GetConfigurationAsync()
	?? throw new ArgumentNullException("App Secrets can not be null.");

var appSecrets = configurationResponse.AppSecrets;

builder.Services.RegisterInfrastructureServices(appSecrets);
builder.Services.ConfigureGoogleMaps(appSecrets);

await builder.Build().RunAsync();