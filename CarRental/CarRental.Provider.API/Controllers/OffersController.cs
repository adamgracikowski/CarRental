using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Requests.Customers.DTOs;
using CarRental.Provider.API.Requests.Offers.Commands;
using CarRental.Provider.API.Requests.Rentals.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class OffersController : ControllerBase
{
    private readonly IMediator mediator;

    public OffersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpPost("{id}")]
    public async Task<Result<RentalDto>> ChooseOffer(int id, CustomerDto customerDto, CancellationToken cancellationToken)
    {
        var command = new ChooseOfferCommand(id, customerDto);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }
}