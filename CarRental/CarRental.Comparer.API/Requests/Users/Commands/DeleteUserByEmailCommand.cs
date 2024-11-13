using Ardalis.Result;
using MediatR;

namespace CarRental.Comparer.API.Requests.Users.Commands;

public sealed record class DeleteUserByEmailCommand(string Email) : IRequest<Result>;