using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Commands;

public sealed record class CreateOfferCommand(
	int Id, 
	int CarId, 
	CreateOfferDto CreateOfferDto
) : IRequest<Result<OfferDto>>;