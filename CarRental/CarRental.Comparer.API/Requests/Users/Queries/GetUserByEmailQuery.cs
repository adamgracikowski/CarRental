using Ardalis.Result;
using CarRental.Comparer.API.DTOs.Users;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Queries;

public sealed record class GetUserByEmailQuery(
	string Email
) : IRequest<Result<UserDto>>;