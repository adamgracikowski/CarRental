namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalDisplayDto(
	string Make,
	string Model,
	int Id,
	string Status,
	DateTime RentedAt,
	DateTime? ReturnedAt,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay
);