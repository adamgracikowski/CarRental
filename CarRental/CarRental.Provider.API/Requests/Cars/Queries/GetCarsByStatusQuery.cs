using Ardalis.Result;
using CarRental.Common.Core.Enums;
using CarRental.Provider.API.Requests.Cars.DTOs;
using MediatR;

namespace CarRental.Provider.API.Requests.Cars.Queries;

public sealed record GetCarsByStatusQuery(CarStatus Status) : IRequest<Result<CarListDto>>;