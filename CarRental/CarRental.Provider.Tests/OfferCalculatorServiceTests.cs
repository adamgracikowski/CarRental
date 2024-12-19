﻿using CarRental.Common.Infrastructure.Providers.DateTimeProvider;
using CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
using CarRental.Provider.Tests.TestData;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace CarRental.Provider.Tests;

public sealed class OfferCalculatorServiceTests
{
	private readonly OfferCalculatorService service;
	private readonly Mock<IDateTimeProvider> mockDateTimeProvider;
	private readonly IOptions<OfferCalculatorOptions> options;

	public OfferCalculatorServiceTests()
	{
		this.mockDateTimeProvider = new Mock<IDateTimeProvider>();
		this.options = Options.Create(new OfferCalculatorOptions
		{
			YoungDriverSurcharge = 0.2m,
			YoungDriverAgeLimit = 25,
			InexperiencedDriverSurcharge = 0.15m,
			InexperiencedDriverYearsLimit = 5,
			OfferExpirationInMinutes = 60
		});

		this.service = new OfferCalculatorService(this.options, this.mockDateTimeProvider.Object);
	}

	[Fact]
	public void CalculatePricePerDay_ShouldSetCorrectExpiration()
	{
		// Arrange
		var input = new OfferCalculatorInput(
			DrivingLicenseYears: default,
			Age: default,
			Latitude: default,
			Longitude: default,
			BaseRentalPricePerDay: default,
			BaseInsurancePricePerDay: default
		);

		var generatedAt = DateTime.Today;
		this.mockDateTimeProvider.Setup(p => p.UtcNow).Returns(generatedAt);

		// Act
		var result = this.service.CalculatePricePerDay(input);

		// Assert
		result.Should().NotBeNull();
		result.GeneratedAt.Should().Be(generatedAt);
		result.ExpiresAt.Should().Be(generatedAt.AddMinutes(this.options.Value.OfferExpirationInMinutes));
	}

	[Theory]
	[ClassData(typeof(OfferCalculatorServiceTheoryData))]
	public void CalculatePricePerDay_ShouldApplySurchargesCorrectly(
		int age,
		int drivingLicenseYears,
		decimal baseRentalPrice,
		decimal baseInsurancePrice,
		decimal expectedRentalPrice,
		decimal expectedInsurancePrice)
	{
		// Arrange
		var input = new OfferCalculatorInput(
			DrivingLicenseYears: drivingLicenseYears,
			Age: age,
			Latitude: 0,
			Longitude: 0,
			BaseRentalPricePerDay: baseRentalPrice,
			BaseInsurancePricePerDay: baseInsurancePrice
		);

		this.mockDateTimeProvider.Setup(p => p.UtcNow).Returns(DateTime.Today);

		// Act
		var result = this.service.CalculatePricePerDay(input);

		// Assert
		result.Should().NotBeNull();
		result.RentalPricePerDay.Should().Be(expectedRentalPrice);
		result.InsurancePricePerDay.Should().Be(expectedInsurancePrice);
	}
}