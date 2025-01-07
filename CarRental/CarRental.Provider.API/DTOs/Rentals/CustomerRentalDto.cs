using CarRental.Common.Core.Enums;
using CarRental.Provider.API.DTOs.Cars;

namespace CarRental.Provider.API.DTOs.Rentals;

public sealed record CustomerRentalDto
{
	public int Id { get; set; }
	public RentalStatus Status { get; set; }
	public CarDto Car { get; set; }
}