using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Commands;

public sealed record class CreateUserCommand(CreateUserDto CreateUserDto) : IRequest<Result<UserDto>>;
