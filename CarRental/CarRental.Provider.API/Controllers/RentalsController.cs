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

namespace CarRental.Provider.API.Controllers;

[Authorize]
[Route("rentals")]
[ApiController]
public sealed class RentalsController : ControllerBase
{
	private readonly IMediator mediator;

	public RentalsController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	/// <summary>
	/// Retrieves the status of a specific rental.
	/// </summary>
	/// <param name="id">The ID of the rental to retrieve the status for.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the rental status.</returns>
	/// <response code="200">The rental status was retrieved successfully.</response>
	/// <response code="403">The user is not authorized to view this rental.</response>
	/// <response code="404">The specified rental was not found.</response>
	/// <response code="500">An internal server error occurred while retrieving the status.</response>
	[TranslateResultToActionResult]
    [HttpGet("{id}/status")]
    public async Task<Result<RentalStatusDto>> GetRentalStatus(int id, CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

		var query = new GetRentalByIdQuery(id, audience);

		var response = await this.mediator.Send(query, cancellationToken);

		return response;
	}

	/// <summary>
	/// Marks a rental as ready for return.
	/// </summary>
	/// <param name="id">The ID of the rental to mark as ready for return.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the updated rental status.</returns>
	/// <response code="200">The rental was successfully marked as ready for return.</response>
	/// <response code="403">The user is not authorized to update this rental.</response>
	/// <response code="404">The specified rental was not found.</response>
	/// <response code="500">An internal server error occurred while updating the rental status.</response>
	[TranslateResultToActionResult]
    [HttpPatch("{id}/ready-for-return")]
    public async Task<Result<RentalStatusDto>> ReturnRental(int id, CancellationToken cancellationToken)
    {
        var audience = User.GetAudience();

		var command = new ReturnRentalCommand(id, audience);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Accepts the return of a specific rental.
	/// </summary>
	/// <param name="id">The ID of the rental to accept the return for.</param>
	/// <param name="acceptRentalReturnDto">The details of the return process.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the details of the accepted return.</returns>
	/// <response code="200">The rental return was successfully accepted.</response>
	/// <response code="400">The provided return details are invalid.</response>
	/// <response code="403">The user is not authorized to accept this return.</response>
	/// <response code="404">The specified rental was not found.</response>
	/// <response code="500">An internal server error occurred while processing the return.</response>
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

	/// <summary>
	/// Confirms a rental based on an ID and confirmation key.
	/// </summary>
	/// <param name="id">The ID of the rental to confirm.</param>
	/// <param name="key">The confirmation key to validate the rental.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>An empty result indicating success or failure.</returns>
	/// <response code="200">The rental was successfully confirmed.</response>
	/// <response code="400">The confirmation key is invalid or expired or the specified rental is not in the unconfirmed state.</response>
	/// <response code="404">The specified rental was not found.</response>
	/// <response code="500">An internal server error occurred while confirming the rental.</response>
	[AllowAnonymous]
    [TranslateResultToActionResult]
    [HttpGet("{id}/confirm/{key}")]
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