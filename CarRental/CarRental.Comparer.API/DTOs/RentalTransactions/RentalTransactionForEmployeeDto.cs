using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.DTOs.CarDetails;
using CarRental.Comparer.API.DTOs.Users;

namespace CarRental.Comparer.API.DTOs.RentalTransactions;

public sealed record RentalTransactionForEmployeeDto
{
	public required int Id { get; init; }
	public required decimal RentalPricePerDay { get; init; }
	public required decimal InsurancePricePerDay { get; init; }
	public required DateTime RentedAt { get; init; }
	public DateTime? ReturnedAt { get; init; }
	public required string Status { get; init; }
	public required UserForEmployeeDto User { get; init; }
	public required CarDetailsForEmployeeDto Car { get; init; }
	public string? Description { get; init; }
	public string? Image { get; init; }
}