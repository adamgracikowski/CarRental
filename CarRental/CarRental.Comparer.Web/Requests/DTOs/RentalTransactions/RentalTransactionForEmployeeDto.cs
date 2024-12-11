using CarRental.Comparer.Web.Requests.DTOs.Cars;
using CarRental.Comparer.Web.Requests.DTOs.Users;

namespace CarRental.Comparer.Web.Requests.DTOs.RentalTransactions;

public sealed record RentalTransactionForEmployeeDto(
	int Id,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay,
	DateTime RentedAt,
	DateTime? ReturnedAt,
	string Status,
	UserForEmployeeDto User,
	CarDetailsForEmployeeDto Car,
	string? Description = null,
	string? Image = null
);