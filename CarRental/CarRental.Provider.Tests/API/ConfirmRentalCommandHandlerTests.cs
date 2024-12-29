using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.API.Requests.Rentals.Commands;
using CarRental.Provider.API.Requests.Rentals.Handlers;
using CarRental.Provider.Infrastructure.EmailServices;
using CarRental.Provider.Persistence.Specifications.Rentals;
using CarRental.Provider.Tests.Dummies;
using FluentAssertions;
using Moq;

namespace CarRental.Provider.Tests.API;

public sealed class ConfirmRentalCommandHandlerTests
{
	private readonly Mock<IRepositoryBase<Rental>> rentalsRepositoryMock;
	private readonly Mock<IEmailService> emailServiceMock;
	private readonly Mock<IDateTimeProvider> dateTimeProviderMock;
	private readonly Mock<IEmailInputMaker> emailInputMakerMock;
	private readonly ConfirmRentalCommandHandler handler;

	public ConfirmRentalCommandHandlerTests()
	{
		rentalsRepositoryMock = new();
		emailServiceMock = new();
		dateTimeProviderMock = new();
		emailInputMakerMock = new();

		handler = new ConfirmRentalCommandHandler(
			rentalsRepositoryMock.Object,
			emailInputMakerMock.Object,
			emailServiceMock.Object,
			dateTimeProviderMock.Object);
	}

	[Fact]
	public async Task Handle_WhenRentalDoesNotExist_ShouldReturnNotFound()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		result.Status.Should().Be(ResultStatus.NotFound);
	}

	[Fact]
	public async Task Handle_WhenOfferHasExpired_ShouldReturnInvalid()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(-1);

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);

		rentalsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().ContainSingle(e => e.Identifier == nameof(Offer.ExpiresAt));
	}

	[Fact]
	public async Task Handle_WhenRentalIsActive_ShouldReturnSuccess()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Status = RentalStatus.Active;
		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(1);

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);

		rentalsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Ok);
	}

	[Fact]
	public async Task Handle_WhenRentalStatusIsNotUnconfirmed_ShouldReturnInvalid()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Status = RentalStatus.Returned;
		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(1);

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);

		rentalsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().ContainSingle(e => e.Identifier == nameof(Rental.Status));
	}

	[Fact]
	public async Task Handle_WhenKeyDoesNotMatch_ShouldReturnInvalid()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: "TestKey");
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Status = RentalStatus.Unconfirmed;
		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(1);
		rental.Offer.Key = "DifferentKey";

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);

		rentalsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().ContainSingle(e => e.Identifier == command.Key);
	}

	[Fact]
	public async Task Handle_WhenCarIsNotAvailable_ShouldReturnInvalid()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Status = RentalStatus.Unconfirmed;
		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(1);
		rental.Offer.Car.Status = CarStatus.Rented;

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);

		rentalsRepositoryMock.VerifyNoOtherCalls();

		result.Status.Should().Be(ResultStatus.Invalid);
		result.ValidationErrors.Should().ContainSingle(e => e.Identifier == nameof(CarStatus));
	}

	[Fact]
	public async Task Handle_ShouldConfirmRentalAndSendEmail_WhenAllConditionsAreMet()
	{
		// Arrange
		var command = new ConfirmRentalCommand(Id: default, Key: string.Empty);
		var rental = ProviderEntitiesDummyFactory.CreateRentalDummy();

		rental.Status = RentalStatus.Unconfirmed;
		rental.Offer.ExpiresAt = DateTime.Today.AddMinutes(1);

		dateTimeProviderMock
			.Setup(d => d.UtcNow).Returns(DateTime.Today);

		rentalsRepositoryMock
			.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
				It.IsAny<CancellationToken>()))
			.ReturnsAsync(rental);

		// Act
		var result = await handler.Handle(command, CancellationToken.None);

		// Assert
		rentalsRepositoryMock.Verify(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdWithOfferCarModelMakeClientSpecification>(),
			It.IsAny<CancellationToken>()),
			Times.Once()
		);
		rentalsRepositoryMock.Verify(r => r.UpdateAsync(rental, It.IsAny<CancellationToken>()), Times.Once);
		rentalsRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
		rentalsRepositoryMock.VerifyNoOtherCalls();

		emailServiceMock.Verify(e => e.SendEmailAsync(It.IsAny<SendEmailInput>()), Times.Once);

		result.Status.Should().Be(ResultStatus.Ok);
	}
}