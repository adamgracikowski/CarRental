using Ardalis.Result;
using MediatR;
using CarRental.Comparer.API.DTOs.RentalTransactions;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Queries;

public sealed record GetRentalTransactionsPagedQuery(
	string Email,
	string Status,
	int PageNumber,
	int PageSize) : IRequest<Result<RentalTransactionPaginatedListDto>>;