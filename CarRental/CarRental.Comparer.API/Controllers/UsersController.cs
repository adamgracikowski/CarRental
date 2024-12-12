using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Core.Roles;
using CarRental.Comparer.API.Authorization;
using CarRental.Comparer.API.DTOs.RentalTransactions;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.RentalTransactions.Queries;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.API.Requests.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Comparer.API.Controllers;

[Authorize(Policy = AuthorizationRoles.User)]
[Route("[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
	private readonly IMediator mediator;

	public UsersController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	/// <summary>
	/// Creates a new user with the provided data.
	/// </summary>
	/// <param name="createUserDto">The data required to create a new user.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The ID of the newly created user.</returns>
	/// <response code="200">The user was successfully created.</response>
	/// <response code="400">The provided user data is invalid.</response>
	/// <response code="403">The user is not authorized to view this account.</response>
	/// <response code="500">An internal server error occurred while creating the user.</response>
	[TranslateResultToActionResult]
	[HttpPost]
	public async Task<Result<UserIdDto>> CreateUser(UserDto createUserDto, CancellationToken cancellationToken)
	{
		var command = new CreateUserCommand(createUserDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Retrieves the details of a user by their email address.
	/// </summary>
	/// <param name="email">The email of the user to retrieve.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>The user details associated with the given email.</returns>
	/// <response code="200">The user was found and details were retrieved successfully.</response>
	/// <response code="404">No user was found with the specified email.</response>
	/// <response code="500">An internal server error occurred while retrieving the user details.</response>
	[TranslateResultToActionResult]
	[HttpGet("{email}")]
	public async Task<Result<UserDto>> GetUser(string email, CancellationToken cancellationToken)
	{
		var query = new GetUserByEmailQuery(email);

		var response = await this.mediator.Send(query, cancellationToken);

		return response;
	}

	/// <summary>
	/// Deletes a user by their email address.
	/// </summary>
	/// <param name="email">The email of the user to delete.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>An empty result indicating success or failure.</returns>
	/// <response code="200">The user was successfully deleted.</response>
	/// <response code="403">The user is not authorized to delete this account.</response>
	/// <response code="404">The user with the specified email does not exist.</response>
	/// <response code="500">An internal server error occurred while deleting the user.</response>
	[TranslateResultToActionResult]
	[HttpDelete("{email}")]
	public async Task<Result> DeleteUserByEmail(string email, CancellationToken cancellationToken)
	{
		var userEmail = User.GetEmailClaim();

		if (email != userEmail)
		{
			return Result.Forbidden();
		}

		var command = new DeleteUserByEmailCommand(email);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Updates the details of a user by their email address.
	/// </summary>
	/// <param name="email">The email of the user whose details are to be updated.</param>
	/// <param name="createUserDto">The updated user details.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>An empty result indicating success or failure.</returns>
	/// <response code="200">The user details were successfully updated.</response>
	/// <response code="400">The provided user data is invalid.</response>
	/// <response code="403">The user is not authorized to update this account.</response>
	/// <response code="404">The user with the specified email does not exist.</response>
	/// <response code="500">An internal server error occurred while updating the user details.</response>
	[TranslateResultToActionResult]
	[HttpPut("{email}")]
	public async Task<Result> EditUserByEmail(string email, UserDto createUserDto, CancellationToken cancellationToken)
	{
		var userEmail = User.GetEmailClaim();

		if (email != userEmail)
		{
			return Result.Forbidden();
		}

		var command = new EditUserByEmailCommand(email, createUserDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	/// <summary>
	/// Retrieves a paginated list of rental transactions by status for the authenticated user.
	/// </summary>
	/// <param name="email">The email of the user whose rental transactions are to be retrieved.</param>
	/// <param name="status">The status of the rental transactions to filter by.</param>
	/// <param name="page">The page number for pagination.</param>
	/// <param name="size">The number of items per page for pagination.</param>
	/// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
	/// <returns>A paginated list of rental transactions matching the specified status.</returns>
	/// <response code="200">The rental transactions were successfully retrieved.</response>
	/// <response code="400">The provided parameters are invalid or missing.</response>
	/// <response code="403">The user is not authorized to view these transactions.</response>
	/// <response code="404">The user with the specified email does not exist.</response>
	/// <response code="500">An internal server error occurred while retrieving the rental transactions.</response>
	[TranslateResultToActionResult]
	[HttpGet("{email}/RentalTransactions/{status}")]
	public async Task<Result<RentalTransactionPaginatedListDto>> GetRentalTransactionsByStatus(
		string email,
		string status,
		[FromQuery] int page,
		[FromQuery] int size,
		CancellationToken cancellationToken)
	{
		var userEmail = User.GetEmailClaim();

		if(email != userEmail)
		{
			return Result<RentalTransactionPaginatedListDto>.Forbidden();
		}

		var query = new GetRentalTransactionsByStatusQuery(email, status, page, size);

		var response = await mediator.Send(query, cancellationToken);

		return response;
	}
}