using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.API.Requests.Providers.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Providers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class ProvidersController : ControllerBase
{
    private readonly IMediator mediator;

    public ProvidersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpPost("{id}/Cars/{carId}/Offers")]
    public async Task<Result<OfferDto>> CreateOffer(int id, int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
    {
        var command = new CreateOfferCommand(id, carId, createOfferDto);

        var response = await mediator.Send(command, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpPost("{id}/Offers/{offerId}")]
    public async Task<Result<RentalTransactionIdWithDateTimesDto>> ChooseOffer(int id, int offerId, ChooseOfferDto chooseOfferDto, CancellationToken cancellationToken)
    {
        var command = new ChooseOfferCommand(id, offerId, chooseOfferDto);

        var response = await mediator.Send(command, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpGet]
    public async Task<Result<ProvidersDto>> GetProviders(CancellationToken cancellationToken)
    {
        var query = new GetProvidersQuery();

        var response = await mediator.Send(query, cancellationToken);

        return response;
    }
}