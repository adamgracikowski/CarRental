using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Cars;
using CarRental.Provider.API.Requests.Cars.Queries;
using CarRental.Provider.Persistence.Specifications.Cars;
using MediatR;

namespace CarRental.Provider.API.Requests.Cars.Handlers;

public sealed class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, Result<CarDetailsExtendedDto>>
{
	private readonly IRepositoryBase<Car> carsRepository;
	private readonly IMapper mapper;

	public GetCarByIdQueryHandler(
		IRepositoryBase<Car> carsRepository,
		IMapper mapper)
	{
		this.carsRepository = carsRepository;
		this.mapper = mapper;
	}

	public async Task<Result<CarDetailsExtendedDto>> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
	{
		var specification = new CarByIdWithModelMakeSpecification(request.Id);

		var car = await this.carsRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (car == null)
		{
			return Result<CarDetailsExtendedDto>.NotFound();
		}

		var carDetailsExtendedDto = this.mapper.Map<CarDetailsExtendedDto>(car);

		return Result.Success(carDetailsExtendedDto);
	}
}