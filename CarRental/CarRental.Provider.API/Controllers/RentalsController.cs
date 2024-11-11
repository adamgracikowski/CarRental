using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.DTOs.Rentals;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.API.Requests.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CarRental.Provider.API.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public sealed class RentalsController : ControllerBase
{
    private readonly IMediator mediator;

    public RentalsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpGet("{id}/status")]
    public async Task<Result<RentalStatusDto>> GetRentalStatus(int id, CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

        var query = new GetRentalByIdQuery(id, audience);

        var response = await this.mediator.Send(query, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpPatch("{id}/ready-for-return")]
    public async Task<Result<RentalStatusDto>> ReturnRental(int id, CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

        var command = new ReturnRentalCommand(id, audience);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpPost("{id}/accept-return")]
    public async Task<Result<RentalReturnDto>> AcceptRentalReturn(
        int id,
        [FromForm] AcceptRentalReturnDto acceptRentalReturnDto, 
        CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

        var command = new AcceptRentalReturnCommand(id, acceptRentalReturnDto, audience);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }

    [AllowAnonymous]
    [TranslateResultToActionResult]
    [HttpPatch("{id}/confirm/{key}")]
    public async Task<Result> ConfirmRental(
    int id,
    string key,
    CancellationToken cancellationToken)
    {
        var command = new ConfirmRentalCommand(id, key);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }

}