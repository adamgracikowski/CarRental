using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Core.Enums;
using CarRental.Provider.API.Requests.Cars.DTOs;
using CarRental.Provider.API.Requests.Cars.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

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
}