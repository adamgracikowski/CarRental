using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Commands;

public sealed record EditUserByEmailCommand(
	string Email, 
	UserDto EditUserDto
) : IRequest<Result>;