namespace CarRental.Provider.API.DTOs.Rentals;

public sealed record CustomerRentalsDto(
	IEnumerable<CustomerRentalDto> Rentals
);