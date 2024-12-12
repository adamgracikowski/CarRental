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

	/// <summary>
	/// Retrieves a list of available cars.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing a list of available cars.</returns>
	/// <response code="200">The list of available cars was retrieved successfully.</response>
	/// <response code="500">An internal server error occurred while retrieving the cars.</response>
	[TranslateResultToActionResult]
    [HttpGet("Available")]
    public async Task<Result<CarListDto>> GetAvailableCars(CancellationToken cancellationToken)
    {
        var query = new GetCarsByStatusQuery(CarStatus.Available);

        var response = await this.mediator.Send(query, cancellationToken);

        return response;
    }

	/// <summary>
	/// Creates an offer for a specific car.
	/// </summary>
	/// <param name="id">The ID of the car for which the offer is being created.</param>
	/// <param name="createOfferDto">The details of the offer to be created.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the details of the created offer.</returns>
	/// <response code="200">The offer was created successfully.</response>
	/// <response code="400">The provided offer details are invalid.</response>
	/// <response code="403">The user is not authorized to create this offer.</response>
	/// <response code="404">The specified car was not found.</response>
	/// <response code="500">An internal server error occurred while creating the offer.</response>
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