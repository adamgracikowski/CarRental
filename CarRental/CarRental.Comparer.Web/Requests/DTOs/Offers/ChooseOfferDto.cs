namespace CarRental.Comparer.Web.Requests.DTOs.Offers;

public sealed record ChooseOfferDto(
	string EmailAddress,
	string Make,
	string Model,
	string Segment,
	string CarOuterId,
	int YearOfProduction,
	string? TransmissionType,
	string? FuelType,
	int NumerOfDoors,
	int NumberOfSeats,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay
);