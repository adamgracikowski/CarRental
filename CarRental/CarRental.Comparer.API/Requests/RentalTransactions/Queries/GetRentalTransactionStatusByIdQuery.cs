using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Queries;

public sealed record class GetRentalTransactionStatusByIdQuery(int rentalTransactionId) : IRequest<Result<RentalTransactionStatusDto>>;
