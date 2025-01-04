namespace CarRental.Comparer.Web.Services.GeolocationServices;

public sealed class GeoLocationService : IGeoLocationService
{
	private readonly Microsoft.JSInterop.IGeolocationService geolocation;

	public GeoLocationService(Microsoft.JSInterop.IGeolocationService geolocation)
	{
		this.geolocation = geolocation;
	}

	public async Task<GeolocationResult> GetCurrentPosition()
	{
		try
		{
			var tcs = new TaskCompletionSource<GeolocationResult>();

			this.geolocation.GetCurrentPosition(
				onSuccessCallback: position =>
				{
					var coords = position.Coords;
					var result = new GeolocationResult(
						new Position((coords.Longitude, coords.Latitude)),
						IsSuccess: true
					);

					tcs.SetResult(result);
				},
				onErrorCallback: error =>
				{
					tcs.SetException(new Exception(error.Message));
				}
			);

			return await tcs.Task;
		}
		catch (Exception)
		{
			return new GeolocationResult(new((default, default)), IsSuccess: false);
		}
	}
}