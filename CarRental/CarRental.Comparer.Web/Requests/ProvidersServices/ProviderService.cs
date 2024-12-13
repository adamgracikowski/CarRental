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

	public async Task<ProviderListDto> GetProvidersAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			return await httpClient.GetFromJsonAsync<ProviderListDto>("providers", cancellationToken)
				?? new ProviderListDto([]);
		}
		catch (Exception)
		{
			return new ProviderListDto([]);
		}
	}
}