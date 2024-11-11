using Ardalis.Result;
using CarRental.Provider.API.DTOs.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Commands;

public sealed record ReturnRentalCommand(int Id, string? Audience) : IRequest<Result<RentalStatusDto>>;