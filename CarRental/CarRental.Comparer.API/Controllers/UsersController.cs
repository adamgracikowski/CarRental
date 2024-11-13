using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using CarRental.Comparer.API.DTOs.Users;
using CarRental.Comparer.API.Requests.Users.Commands;
using CarRental.Comparer.API.Requests.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Provider.API.Controllers;

[Route("[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [TranslateResultToActionResult]
    [HttpPost]
    public async Task<Result<UserDto>> CreateUser(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(createUserDto);

        var response = await _mediator.Send(command, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpGet("{email}")]
    public async Task<Result<CreateUserDto>> GetUser(string email, CancellationToken cancellationToken)
    {
        var query = new GetUserByEmailQuery(email);

        var response = await _mediator.Send(query, cancellationToken);

        return response;
    }

    [TranslateResultToActionResult]
    [HttpDelete("{email}")]
    public async Task<Result> DeleteUserByEmail(string email, CancellationToken cancellationToken)
    {
        var command = new DeleteUserByEmailCommand(email);

        var response = await _mediator.Send(command, cancellationToken);

        return response;
    }

}