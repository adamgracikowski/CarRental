using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Settings.ConfigurationServices
{
	public sealed class ConfigurationService
	{
		private readonly HttpClient httpClient;

		public ConfigurationService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<ConfigurationResponseDto?> GetConfigurationAsync()
		{
			return await this.httpClient.GetFromJsonAsync<ConfigurationResponseDto>("configurations");
		}
	}
}