namespace CarRental.Comparer.Web.Services.GeolocationServices;

public interface IGeoLocationService
{
	Task<GeolocationResult> GetCurrentPosition();
}