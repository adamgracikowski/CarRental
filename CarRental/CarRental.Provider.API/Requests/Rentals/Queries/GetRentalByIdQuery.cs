using Ardalis.Result;
using CarRental.Provider.API.DTOs.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Queries;

public sealed record GetRentalByIdQuery(int Id, string? Audience) : IRequest<Result<RentalStatusDto>>;