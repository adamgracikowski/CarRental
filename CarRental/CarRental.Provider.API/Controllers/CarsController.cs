using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Core.Enums;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.DTOs.Cars;
using CarRental.Provider.API.DTOs.Offers;
using CarRental.Provider.API.Requests.Cars.Queries;
using CarRental.Provider.API.Requests.Offers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public sealed class CarsController : ControllerBase
{
    private readonly IMediator mediator;

    public CarsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpGet("Available")]
    public async Task<Result<CarListDto>> GetAvailableCars(CancellationToken cancellationToken)
    {
        var query = new GetCarsByStatusQuery(CarStatus.Available);

        var response = await this.mediator.Send(query, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpPost("{id}/Offers")]
    public async Task <Result<OfferDto>> CreateOffer(int id, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

        var command = new CreateOfferCommand(id, createOfferDto, audience);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }
}