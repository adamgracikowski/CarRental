using Microsoft.AspNetCore.Http;

namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;

public sealed record AcceptRentalReturnDto(
	string? Description,
	IFormFile? Image,
	decimal Latitude,
	decimal Longitude
);