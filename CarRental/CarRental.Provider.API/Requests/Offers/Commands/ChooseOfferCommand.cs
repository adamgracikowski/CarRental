using Ardalis.Result;
using CarRental.Provider.API.DTOs.Customers;
using CarRental.Provider.API.DTOs.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Offers.Commands;

public sealed record ChooseOfferCommand(
	int Id, 
	CustomerDto CustomerDto, 
	string? Audience
) : IRequest<Result<RentalDto>>;