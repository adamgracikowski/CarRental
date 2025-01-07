using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Provider.API.Authorization;
using CarRental.Provider.API.DTOs.Rentals;
using CarRental.Provider.API.Requests.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Authorize]
[Route("customers")]
[ApiController]
public sealed class CustomersController : ControllerBase
{
	private readonly IMediator mediator;

	public CustomersController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	/// <summary>
	/// Retrievs a list of rentals of a specific customer.
	/// </summary>
	/// <param name="emailAddress">The email address of the customer.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A result containing a list of rentals of a specific customer.</returns>
	/// <response code="200">The rentals of the customer were retrieved successfully.</response>
	/// <response code="404">The specified customer was not found.</response>
	/// <response code="500">An internal server error occurred while retrieving the rentals of the customer.</response>
	[TranslateResultToActionResult]
	[HttpGet("{emailAddress}/rentals")]
	public async Task<Result<CustomerRentalsDto>> GetCustomerRentals(string emailAddress, CancellationToken cancellationToken)
	{
		var audience = User.GetAudience();

		var query = new GetCustomerRentalsQuery(emailAddress, audience);

		var response = await this.mediator.Send(query, cancellationToken);

		return response;
	}
}