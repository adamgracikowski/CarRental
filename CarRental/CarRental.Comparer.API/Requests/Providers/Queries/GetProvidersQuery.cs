using Ardalis.Result;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Providers;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Queries;

public sealed record class GetProvidersQuery() : IRequest<Result<ProvidersDto>>;
