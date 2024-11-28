using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.DTOs.Customers;
using CarRental.Provider.API.Requests.Offers.Commands;
using CarRental.Provider.API.Requests.Rentals.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Authorize]
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
        var audience = User.GetAudience();

        var command = new ChooseOfferCommand(id, customerDto, audience);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }
}