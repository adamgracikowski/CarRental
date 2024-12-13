using Ardalis.Result;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Commands;

public sealed record ConfirmRentalCommand(
	int Id, 
	string Key
) : IRequest<Result>;