using Ardalis.Result;
using CarRental.Provider.API.DTOs.Rentals;
using MediatR;

namespace CarRental.Provider.API.Requests.Rentals.Queries;

public sealed record GetCustomerRentalsQuery(
	string EmailAddress,
	string Audience
) : IRequest<Result<CustomerRentalsDto>>;