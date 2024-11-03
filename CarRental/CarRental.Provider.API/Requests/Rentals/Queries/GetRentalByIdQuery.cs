using Ardalis.Result;
using CarRental.Provider.API.Requests.Rentals.DTOs;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Queries;

public sealed record GetRentalByIdQuery(int Id) : IRequest<Result<RentalStatusDto>>;