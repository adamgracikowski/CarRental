using Ardalis.Result;
using CarRental.Provider.API.DTOs.Cars;
using MediatR;

namespace CarRental.Provider.API.Requests.Cars.Queries;

public sealed record GetCarByIdQuery(int Id) : IRequest<Result<CarDetailsExtendedDto>>;