using CarRental.Common.Core.Enums;

namespace CarRental.Provider.API.DTOs.Cars;

public sealed record CarDetailsExtendedDto
{
	public int ProductionYear { get; set; }
	public FuelType FuelType { get; set; }
	public TransmissionType TransmissionType { get; set; }
	public decimal Longitude { get; set; }
	public decimal Latitude { get; set; }
	public string Model { get; set; } = string.Empty;
	public string Make { get; set; } = string.Empty;
}