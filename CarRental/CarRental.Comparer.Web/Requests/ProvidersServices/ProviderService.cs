using CarRental.Comparer.Web.Requests.DTOs.Providers;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.ProvidersServices;

public class ProviderService : IProviderService
{
	private readonly HttpClient httpClient;

	public ProviderService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<ProviderListDto> GetProvidersAsync()
	{
		try
		{
			return await httpClient.GetFromJsonAsync<ProviderListDto>("Providers")
				?? new ProviderListDto([]);
		}
		catch (Exception)
		{
			return new ProviderListDto([]);
		}
	}
}