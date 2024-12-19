using Ardalis.Specification;
using CarRental.Common.Core.Enums;
using CarRental.Common.Core.ProviderEntities;
using CarRental.Provider.Infrastructure.BackgroundJobs.RentalServices;
using CarRental.Provider.Persistence.Specifications.Rentals;
using CarRental.Provider.Tests.Dummies;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarRental.Provider.Tests;

public class RentalStatusCheckerServiceTests
{
	private readonly Mock<IRepositoryBase<Rental>> rentalsRepositoryMock;
	private readonly Mock<ILogger<RentalStatusCheckerService>> loggerMock;
	private readonly RentalStatusCheckerService service;

	public RentalStatusCheckerServiceTests()
	{
		this.rentalsRepositoryMock = new Mock<IRepositoryBase<Rental>>();		
		this.loggerMock = new Mock<ILogger<RentalStatusCheckerService>>();
		this.service = new RentalStatusCheckerService(
			this.rentalsRepositoryMock.Object, 
			this.loggerMock.Object
		);
	}

	[Fact]
	public async Task CheckAndUpdateRentalStatusAsync_WhenRentalDoesNotExist_ShouldLogAndReturn()
	{
		// Arrange
		rentalsRepositoryMock.Setup(r => r.FirstOrDefaultAsync(
			It.IsAny<RentalByIdSpecification>(), It.IsAny<CancellationToken>())
		).ReturnsAsync((Rental)null!);

		// Act
		await service.CheckAndUpdateRentalStatusAsync(default, CancellationToken.None);

		// Assert
		loggerMock.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((o, t) => true),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once
		);

		rentalsRepositoryMock.Verify(
			r => r.UpdateAsync(It.IsAny<Rental>(), It.IsAny<CancellationToken>()),
			Times.Never
		);
	}

	[Fact]
	public async Task CheckAndUpdateRentalStatusAsync_WhenRentalStatusIsNotUnconfirmed_ShouldLogAndReturn()
	{
		// Arrange
		var rental = ProviderEntitiesDummyFactory.CreateDummyRental();
		rental.Status = RentalStatus.Active;

		rentalsRepositoryMock.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdSpecification>(), It.IsAny<CancellationToken>())
		).ReturnsAsync(rental);

		// Act
		await service.CheckAndUpdateRentalStatusAsync(rental.Id, CancellationToken.None);

		// Assert
		loggerMock.Verify(
			x => x.Log(
				LogLevel.Information,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((o, t) => true),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
			Times.Once
		);

		rentalsRepositoryMock.Verify(
			r => r.UpdateAsync(It.IsAny<Rental>(), It.IsAny<CancellationToken>()),
			Times.Never
		);
	}

	[Fact]
	public async Task CheckAndUpdateRentalStatusAsync_WhenStatusIsUnconfirmed_ShouldSetStatusToRejected()
	{
		// Arrange
		var rental = ProviderEntitiesDummyFactory.CreateDummyRental();
		rental.Status = RentalStatus.Unconfirmed;

		rentalsRepositoryMock.Setup(r => r.FirstOrDefaultAsync(
				It.IsAny<RentalByIdSpecification>(), It.IsAny<CancellationToken>())
		).ReturnsAsync(rental);

		// Act
		await service.CheckAndUpdateRentalStatusAsync(default, CancellationToken.None);

		// Assert
		rental.Status.Should().Be(RentalStatus.Rejected);

		rentalsRepositoryMock.Verify(
			r => r.UpdateAsync(rental, It.IsAny<CancellationToken>()),
			Times.Once
		);

		rentalsRepositoryMock.Verify(
			r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), 
			Times.Once
		);
	}
}