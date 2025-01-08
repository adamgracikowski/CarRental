using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.API.Authorization.Roles;
using CarRental.Comparer.API.Requests.Providers.Commands;
using CarRental.Comparer.API.Requests.Providers.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Providers;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.RentalTransactions;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers;
using CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Route("providers")]
[ApiController]
public sealed class ProvidersController : ControllerBase
{
	private readonly IMediator mediator;

	public ProvidersController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	/// <summary>
	/// Creates a new offer for a specific car provided by a rental provider.
	/// </summary>
	/// <param name="id">The ID of the rental provider.</param>
	/// <param name="carId">The ID of the car for which the offer is created.</param>
	/// <param name="createOfferDto">The data required to create the offer.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The created offer, or a not found or error result.</returns>
	/// <response code="200">The offer was successfully created.</response>
	/// <response code="404">The specified rental provider or car was not found.</response>
	/// <response code="500">An internal server error occurred while creating the offer.</response>
	[TranslateResultToActionResult]
	[HttpPost("{id}/cars/{carId}/offers")]
	public async Task<Result<OfferDto>> CreateOffer(int id, int carId, CreateOfferDto createOfferDto, CancellationToken cancellationToken)
	{
		var command = new CreateOfferCommand(id, carId, createOfferDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Allows an authorized user to choose an offer for a specific rental provider.
	/// </summary>
	/// <param name="id">The ID of the rental provider.</param>
	/// <param name="offerId">The ID of the offer to choose.</param>
	/// <param name="chooseOfferDto">The data required to choose the offer.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The transaction ID and associated dates.</returns>
	/// <response code="200">The offer was successfully chosen.</response>
	/// <response code="403">The user is not authorized to choose this offer.</response>
	/// <response code="400">The request data was invalid.</response>
	/// <response code="500">An internal server error occurred while choosing the offer.</response>
	[Authorize(Policy = AuthorizationRoles.User)]
	[TranslateResultToActionResult]
	[HttpPost("{id}/offers/{offerId}")]
	public async Task<Result<RentalTransactionIdWithDateTimesDto>> ChooseOffer(int id, int offerId, ChooseOfferDto chooseOfferDto, CancellationToken cancellationToken)
	{
		var command = new ChooseOfferCommand(id, offerId, chooseOfferDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Retrieves a list of all car rental providers.
	/// </summary>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A list of car rental providers.</returns>
	/// <response code="200">The list of providers was retrieved successfully.</response>
	/// <response code="500">An internal server error occurred while retrieving the providers.</response>
	[TranslateResultToActionResult]
	[HttpGet]
	public async Task<Result<ProvidersDto>> GetProviders(CancellationToken cancellationToken)
	{
		var query = new GetProvidersQuery();

		var response = await this.mediator.Send(query, cancellationToken);

		return response;
	}
}