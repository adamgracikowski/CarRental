using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.API.DTOs.Cars;
using CarRental.Provider.API.DTOs.Offers;
using CarRental.Provider.API.DTOs.Segments;
using CarRental.Provider.API.Requests.Offers.Commands;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Persistence.Specifications.Cars;
using CarRental.Common.Infrastructure.Providers.RandomStringProvider;
using FluentValidation;
using MediatR;

namespace CarRental.Provider.API.Requests.Offers.Handlers;

public sealed class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, Result<OfferDto>>
{
	private readonly IRepositoryBase<Car> carsRepository;
	private readonly IValidator<CreateOfferCommand> validator;
	private readonly IOfferCalculatorService offerCalculatorService;
	private readonly IRandomStringProvider randomStringProvider;
	private readonly IMapper mapper;

	public CreateOfferCommandHandler(
		IRepositoryBase<Car> carsRepository,
		IValidator<CreateOfferCommand> validator,
		IOfferCalculatorService offerCalculatorService,
		IRandomStringProvider randomStringProvider,
		IMapper mapper
		)
	{
		this.carsRepository = carsRepository;
		this.validator = validator;
		this.offerCalculatorService = offerCalculatorService;
		this.randomStringProvider = randomStringProvider;
		this.mapper = mapper;
	}

	public async Task<Result<OfferDto>> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
	{
		var validation = await this.validator.ValidateAsync(request, cancellationToken);

		if (!validation.IsValid)
		{
			return Result<OfferDto>.Invalid(validation.AsErrors());
		}

		var specification = new CarByIdWithModelSegmentInsuranceSpecification(request.CarId);

		var car = await this.carsRepository.FirstOrDefaultAsync(specification, cancellationToken);

		if (car == null)
		{
			return Result<OfferDto>.NotFound();
		}

		if (car.Status != CarStatus.Available)
		{
			return Result<OfferDto>.Invalid(new ValidationError(nameof(Car.Status), "The car is currently unavailable."));
		}

		var offerCalculatorInput = new OfferCalculatorInput(
			DrivingLicenseYears: request.CreateOfferDto.DrivingLicenseYears,
			Age: request.CreateOfferDto.Age,
			Latitude: request.CreateOfferDto.Latitude,
			Longitude: request.CreateOfferDto.Longitude,
			BaseRentalPricePerDay: car.Model.Segment.PricePerDay,
			BaseInsurancePricePerDay: car.Model.Segment.Insurance.PricePerDay
		);

		var offerCalculatoResult = this.offerCalculatorService.CalculatePricePerDay(offerCalculatorInput);

		var offer = new Offer
		{
			Car = car,
			GeneratedAt = offerCalculatoResult.GeneratedAt,
			ExpiresAt = offerCalculatoResult.ExpiresAt,
			RentalPricePerDay = offerCalculatoResult.RentalPricePerDay,
			InsurancePricePerDay = offerCalculatoResult.InsurancePricePerDay,
			Key = randomStringProvider.GenerateRandomString(),
			GeneratedBy = request.Audience!,
		};

		car.Offers.Add(offer);

		await this.carsRepository.UpdateAsync(car, cancellationToken);
		await this.carsRepository.SaveChangesAsync(cancellationToken);

		var offerDto = new OfferDto(
			Id: offer.Id,
			RentalPricePerDay: offer.RentalPricePerDay,
			InsurancePricePerDay: offer.InsurancePricePerDay,
			GeneratedAt: offer.GeneratedAt,
			ExpiresAt: offer.ExpiresAt,
			Car: this.mapper.Map<CarDetailsDto>(car),
			Segment: this.mapper.Map<SegmentDto>(car.Model.Segment)
		);

		return Result<OfferDto>.Created(offerDto);
	}
}