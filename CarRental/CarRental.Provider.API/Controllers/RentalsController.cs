using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Requests.Rentals.DTOs;
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
}