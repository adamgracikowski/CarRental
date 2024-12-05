using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Common.Core.Roles;
using CarRental.Comparer.API.DTOs.Users;
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

	[TranslateResultToActionResult]
	[HttpPost]
	public async Task<Result<UserIdDto>> CreateUser(UserDto createUserDto, CancellationToken cancellationToken)
	{
		var command = new CreateUserCommand(createUserDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	[TranslateResultToActionResult]
	[HttpGet("{email}")]
	public async Task<Result<UserDto>> GetUser(string email, CancellationToken cancellationToken)
	{
		var query = new GetUserByEmailQuery(email);

		var response = await this.mediator.Send(query, cancellationToken);

		return response;
	}

	[TranslateResultToActionResult]
	[HttpDelete("{email}")]
	public async Task<Result> DeleteUserByEmail(string email, CancellationToken cancellationToken)
	{
		var command = new DeleteUserByEmailCommand(email);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}

	[TranslateResultToActionResult]
	[HttpPut("{email}")]
	public async Task<Result> EditUserByEmail(string email, UserDto createUserDto, CancellationToken cancellationToken)
	{
		var command = new EditUserByEmailCommand(email, createUserDto);

		var response = await this.mediator.Send(command, cancellationToken);

		return response;
	}
}