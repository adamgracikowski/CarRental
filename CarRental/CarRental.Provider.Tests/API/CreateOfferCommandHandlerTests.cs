using Ardalis.Result;
using Ardalis.Specification;
using AutoMapper;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.RandomStringProvider;
using CarRental.Provider.API.DTOs.Offers;
using CarRental.Provider.API.Requests.Offers.Commands;
using CarRental.Provider.API.Requests.Offers.Handlers;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Persistence.Specifications.Cars;
using CarRental.Provider.Tests.Dummies;
using FluentAssertions;
using Moq;

namespace CarRental.Provider.Tests.API;

public sealed class CreateOfferCommandHandlerTests
{
	private readonly Mock<IRepositoryBase<Car>> carsRepositoryMock;
	private readonly Mock<IOfferCalculatorService> offerCalculatorMock;
	private readonly Mock<IRandomStringProvider> randomStringProviderMock;
	private readonly Mock<IMapper> mapperMock;
	private readonly CreateOfferCommandHandler handler;
	private readonly CreateOfferCommand createOfferCommand;

	public CreateOfferCommandHandlerTests()
	{
		carsRepositoryMock = new();
		offerCalculatorMock = new();
		randomStringProviderMock = new();
		mapperMock = new();

		handler = new CreateOfferCommandHandler(
			carsRepositoryMock.Object,
			offerCalculatorMock.Object,
			randomStringProviderMock.Object,
			mapperMock.Object);

		createOfferCommand = new CreateOfferCommand(
			CarId: default,
			CreateOfferDto: new CreateOfferDto(
				DrivingLicenseYears: default,
				Age: default,
				Latitude: default,
				Longitude: default
			),
			Audience: string.Empty
		);
	}

	[Fact]
	public async Task Handle_WhenCarDoesNotExist_ShouldReturnNotFound()
	{
		// Act
		var result = await handler.Handle(createOfferCommand, CancellationToken.None);

		// Assert
		carsRepositoryMock.Verify(c => c.FirstOrDefaultAsync(
			It.IsAny<CarByIdWithModelSegmentInsuranceSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		carsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.NotFound);
	}

	[Fact]
	public async Task Handle_WhenCarIsUnavailable_ShouldReturnInvalid()
	{
		// Arrange
		var car = ProviderEntitiesDummyFactory.CreateCarDummy();

		car.Status = CarStatus.Rented;

		carsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<CarByIdWithModelSegmentInsuranceSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(car);

		// Act
		var result = await handler.Handle(createOfferCommand, CancellationToken.None);

		// Assert
		carsRepositoryMock.Verify(c => c.FirstOrDefaultAsync(
			It.IsAny<CarByIdWithModelSegmentInsuranceSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);

		carsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().ContainSingle(e => e.Identifier == nameof(Car.Status));
	}

	[Fact]
	public async Task Handle_WhenAllConditionsAreMet_ShouldCreateAndReturnOffer()
	{
		// Arrange
		var car = ProviderEntitiesDummyFactory.CreateCarDummy();

		var offerCalculatorResult = new OfferCalculatorResult(
			RentalPricePerDay: default,
			InsurancePricePerDay: default,
			GeneratedAt: DateTime.Today,
			ExpiresAt: DateTime.Today.AddMinutes(1)
		);

		carsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<CarByIdWithModelSegmentInsuranceSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(car);

		offerCalculatorMock
			.Setup(c => c.CalculatePricePerDay(It.IsAny<OfferCalculatorInput>()))
			.Returns(offerCalculatorResult);

		// Act
		var result = await handler.Handle(createOfferCommand, CancellationToken.None);

		// Assert
		carsRepositoryMock.Verify(c => c.FirstOrDefaultAsync(
			It.IsAny<CarByIdWithModelSegmentInsuranceSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once
		);
		carsRepositoryMock.Verify(r => r.UpdateAsync(car, It.IsAny<CancellationToken>()), Times.Once);
		carsRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		carsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Created);
		result.Value.Should().NotBeNull();

		car.Offers.Should().ContainSingle();
	}
}