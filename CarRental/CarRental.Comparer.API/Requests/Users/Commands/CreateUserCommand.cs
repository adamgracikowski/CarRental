using Ardalis.Result;
using CarRental.Comparer.API.Requests.Users.DTOs;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Commands;

public sealed record class CreateUserCommand(CreateUserDto createUserDto) : IRequest<Result<UserDto>>;
