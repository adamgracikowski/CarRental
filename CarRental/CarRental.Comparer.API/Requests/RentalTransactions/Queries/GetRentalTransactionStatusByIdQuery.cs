using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Queries;

public sealed record GetRentalTransactionStatusByIdQuery(
	int RentalTransactionId
) : IRequest<Result<RentalTransactionStatusDto>>;