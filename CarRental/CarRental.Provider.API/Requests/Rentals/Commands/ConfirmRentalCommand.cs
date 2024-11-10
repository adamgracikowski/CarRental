using Ardalis.Result;
using CarRental.Provider.API.DTOs.RentalReturns;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Commands;

public sealed record ConfirmRentalCommand(int Id, string Key) : IRequest<Result>;