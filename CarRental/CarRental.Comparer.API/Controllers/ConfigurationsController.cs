using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.API.Settings;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("configurations")]
[ApiController]
public sealed class ConfigurationsController : ControllerBase
{
	private readonly IConfiguration configuration;

	public ConfigurationsController(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

	[TranslateResultToActionResult]
	[HttpGet]
	public async Task<Result<ConfigurationResponseDto>> GetConfiguration()
	{
		var auth = this.configuration
			.GetSection(AuthSettings.SectionName).Get<AuthSettings>();
		var azureAd = this.configuration
			.GetSection(BlazorAzureAdSettings.SectionName).Get<BlazorAzureAdSettings>();
		var comparerApiSettings = this.configuration
			.GetSection(ComparerApiSettings.SectionName).Get<ComparerApiSettings>();
		var googleMaps = this.configuration
			.GetSection(GoogleMapsSettings.SectionName).Get<GoogleMapsSettings>();

		var appSecrets = new AppSecrets
		{
			Auth = auth,
			AzureAd = azureAd,
			ComparerApiSettings = comparerApiSettings,
			GoogleMaps = googleMaps
		};


		return Result<ConfigurationResponseDto>.Success(await Task.FromResult(new ConfigurationResponseDto(appSecrets)));
	}
}