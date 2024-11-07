using Ardalis.Result;
using CarRental.Provider.API.Requests.Offers.DTOs;
using MediatR;

namespace CarRental.Provider.API.Requests.Offers.Commands;

public sealed record CreateOfferCommand(int CarId, CreateOfferDto CreateOfferDto) : IRequest<Result<OfferDto>>;