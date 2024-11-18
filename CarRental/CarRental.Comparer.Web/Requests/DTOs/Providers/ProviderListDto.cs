namespace CarRental.Comparer.Web.Requests.DTOs.Providers;

public sealed record ProviderListDto(
    ICollection<ProviderDto> Providers
);