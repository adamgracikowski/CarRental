namespace CarRental.Comparer.Infrastructure.CarProviders.ExternalCarProvider.DTOs.Models;

public sealed record class ExternalModelDto(
	int ModelId,
	string ModelName,
	int SeatCount,
	int DoorCount,
	string EngineType,
	int Horsepower,
	int Torque,
	int Length,
	int Width,
	int Height
);