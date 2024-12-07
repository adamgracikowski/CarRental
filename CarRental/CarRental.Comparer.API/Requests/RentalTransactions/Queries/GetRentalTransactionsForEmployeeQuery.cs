using Ardalis.Result;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Queries;

public sealed record GetRentalTransactionsForEmployeeQuery(
	string? EmployeeEmail,
	string Status,
	int Page,
	int Size
) : IRequest<Result<RentalTransactionsForEmployeePaginatedDto>>;