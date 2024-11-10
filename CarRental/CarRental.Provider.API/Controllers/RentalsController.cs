using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.DTOs.RentalReturns;
using CarRental.Provider.API.DTOs.Rentals;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.API.Requests.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

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
        var query = new GetRentalByIdQuery(id);

        var response = await this.mediator.Send(query, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpPatch("{id}/ready-for-return")]
    public async Task<Result<RentalStatusDto>> ReturnRental(int id, CancellationToken cancellationToken)
    {
        var command = new ReturnRentalCommand(id);

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
        var command = new AcceptRentalReturnCommand(id, acceptRentalReturnDto);

        var response = await this.mediator.Send(command, cancellationToken);

        return response;
    }

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