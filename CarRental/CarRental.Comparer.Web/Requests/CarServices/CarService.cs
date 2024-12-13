using CarRental.Comparer.Web.Requests.DTOs.Makes;
using CarRental.Comparer.Web.Requests.DTOs.Models;
using System.Net.Http.Json;

namespace CarRental.Comparer.Web.Requests.CarServices;

public class CarService : ICarService
{
	private readonly HttpClient httpClient;

	public CarService(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<MakeListDto> GetAvailableCars(CancellationToken cancellationToken = default)
	{
		try
		{
			return await this.httpClient.GetFromJsonAsync<MakeListDto>("cars/available")
				?? new MakeListDto([]);
		}
		catch (Exception)
		{
			return new MakeListDto([]);
		}
	}

	public async Task<ModelDetailsDto?> GetModelDetailsAsync(
		string makeName,
		string modelName,
		IEqualityComparer<string> equalityComparer,
		CancellationToken cancellationToken = default)
	{
		var availableCars = await this.GetAvailableCars(cancellationToken);

		var make = availableCars.Makes
			.FirstOrDefault(mk => equalityComparer.Equals(mk.Name, makeName));

		if (make is null)
		{
			return null;
		}

		var model = make.Models
			.FirstOrDefault(m => equalityComparer.Equals(m.Name, modelName));

		if (model is null)
		{
			return null;
		}

		var modelDetails = new ModelDetailsDto(
			Name: model.Name,
			NumberOfDoors: model.NumberOfDoors,
			NumberOfSeats: model.NumberOfSeats,
			EngineType: model.EngineType,
			WheelDriveType: model.WheelDriveType,
			Cars: [.. model.Cars],
			LogoUrl: make.LogoUrl
		);

		return modelDetails;
	}
}