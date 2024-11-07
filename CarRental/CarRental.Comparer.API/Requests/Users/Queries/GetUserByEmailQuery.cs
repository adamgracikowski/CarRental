using Ardalis.Result;
using CarRental.Comparer.API.Requests.Users.DTOs;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Queries;

public sealed record class GetUserByEmailQuery(string email) : IRequest<Result<CreateUserDto>>;
