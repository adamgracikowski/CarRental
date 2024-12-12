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

	/// <summary>
	/// Chooses a specific offer and creates a rental based on the selected offer.
	/// </summary>
	/// <param name="id">The ID of the offer to be chosen.</param>
	/// <param name="customerDto">The details of the customer selecting the offer.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing the details of the created rental.</returns>
	/// <response code="200">The offer was successfully chosen, and a rental was created.</response>
	/// <response code="400">The provided customer details are invalid.</response>
	/// <response code="403">The user is not authorized to choose this offer.</response>
	/// <response code="404">The specified offer was not found.</response>
	/// <response code="500">An internal server error occurred while processing the request.</response>
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