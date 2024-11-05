using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.Requests.Cars.DTOs;
using CarRental.Provider.API.Requests.Cars.Queries;
using CarRental.Provider.API.Requests.Makes.DTOs;
using CarRental.Provider.API.Requests.Models.DTOs;
using CarRental.Provider.Persistence.Specifications.Cars;
using MediatR;

namespace CarRental.Provider.API.Requests.Cars.Handlers;

public sealed class GetCarsByStatusQueryHandler : IRequestHandler<GetCarsByStatusQuery, Result<CarListDto>>
{
    private readonly IRepositoryBase<Car> carsRepository;

    public GetCarsByStatusQueryHandler(IRepositoryBase<Car> carsRepository)
    {
        this.carsRepository = carsRepository;
    }

    public async Task<Result<CarListDto>> Handle(GetCarsByStatusQuery request, CancellationToken cancellationToken)
    {
        var specification = new CarsByStatusWithModelMakeSpecification(request.Status);

        var cars = await this.carsRepository.ListAsync(specification, cancellationToken);

        var makeDtos = cars
            .GroupBy(car => car.Model.Make)
            .Select(makeGroup => new MakeDto(
                Name: makeGroup.Key.Name,
                Models: makeGroup
                    .GroupBy(car => car.Model)
                    .Select(modelGroup => new ModelDto(
                        Name: modelGroup.Key.Name,
                        NumberOfDoors: modelGroup.Key.NumberOfDoors,
                        NumberOfSeats: modelGroup.Key.NumberOfSeats,
                        EngineType: modelGroup.Key.EngineType,
                        WheelDriveType: modelGroup.Key.WheelDriveType,
                        Cars: modelGroup.Select(car => new CarDto(
                            Id: car.Id,
                            ProductionYear: car.ProductionYear
                        )).ToList()
                    )).ToList()
            )).ToList();

        var carListDto = new CarListDto(makeDtos);

        return Result.Success(carListDto);
    }
}