using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Commands;

public sealed record class ChooseOfferCommand(int id, int offerId, ChooseOfferDto chooseOfferDto) : IRequest<Result<RentalIdDto>>;
