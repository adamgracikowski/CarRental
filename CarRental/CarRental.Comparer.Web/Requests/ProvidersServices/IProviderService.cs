using CarRental.Comparer.Web.Requests.DTOs.Providers;

namespace CarRental.Comparer.Web.Requests.ProvidersServices;

public interface IProviderService
{
	Task<ProviderListDto> GetProvidersAsync(CancellationToken cancellationToken = default);
}