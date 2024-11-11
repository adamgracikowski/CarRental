using Ardalis.Result;
using CarRental.Provider.API.DTOs.Offers;
using MediatR;

namespace CarRental.Provider.API.Requests.Offers.Commands;

public sealed record CreateOfferCommand(int CarId, CreateOfferDto CreateOfferDto, string? Audience) : IRequest<Result<OfferDto>>;