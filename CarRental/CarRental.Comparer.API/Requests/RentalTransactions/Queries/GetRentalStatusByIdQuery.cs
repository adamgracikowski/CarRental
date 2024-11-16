using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using MediatR;

namespace CarRental.Comparer.API.Requests.RentalTransactions.Queries;

public sealed record class GetRentalStatusByIdQuery(int rentalId) : IRequest<Result<RentalStatusDto>>;
