using CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;
using FluentAssertions;

namespace CarRental.Provider.Tests;

public sealed class RentalBillCalculatorServiceTests
{
	private readonly RentalBillCalculatorService service;

	public RentalBillCalculatorServiceTests()
	{
		this.service = new RentalBillCalculatorService();
	}

	[Fact]
	public void CalculateBill_WhenReturnedBeforeRented_ShouldThrowArgumentException()
	{
		// Arrange
		var input = new RentalBillCalculatorInput(
			RentedAt: DateTime.Today,
			ReturnedAt: DateTime.Today.AddDays(-1),
			RentalPricePerDay: 0,
			InsurancePricePerDay: 0
		);

		// Act
		Action act = () => this.service.CalculateBill(input);

		// Assert
		act.Should().Throw<ArgumentException>();
	}

	[Theory]
	[InlineData("2024-12-19T00:00:00", "2024-12-19T10:00:00", 1)] // 10 hours, rounds to 1 day
	[InlineData("2024-12-19T00:00:00", "2024-12-20T10:00:00", 2)] // 1 day 10 hours, rounds to 2 days
	[InlineData("2024-12-19T00:00:00", "2024-12-21T10:00:00", 3)] // 2 days 10 hours, rounds to 3 days
	public void CalculateBill_WhenPartialDaysArePresent_ShouldRoundUpNumberOfDays(
		string rentedAt, 
		string returnedAt, 
		int expectedDays)
	{
		// Arrange
		var input = new RentalBillCalculatorInput(
			RentedAt: DateTime.Parse(rentedAt),
			ReturnedAt: DateTime.Parse(returnedAt),
			RentalPricePerDay: 0,
			InsurancePricePerDay: 0
		);

		// Act
		var result = service.CalculateBill(input);

		// Assert
		result.Should().NotBeNull();
		result.NumberOfDays.Should().Be(expectedDays);
	}

	[Theory]
	[InlineData(4, 50.0, 20.0)]
	[InlineData(2, 100.0, 30.0)]
	[InlineData(1, 70.0, 25.0)]
	public void CalculateBill_WhenInputIsValid_ShouldReturnCorrectTotalPrices(
		int numberOfDays,
		decimal rentalPricePerDay,
		decimal insurancePricePerDay)
	{
		// Arrange
		var input = new RentalBillCalculatorInput(
			RentedAt: DateTime.Today.AddDays(-numberOfDays),
			ReturnedAt: DateTime.Today,
			RentalPricePerDay: rentalPricePerDay,
			InsurancePricePerDay: insurancePricePerDay
		);

		var expectedRentalTotalPrice = numberOfDays * rentalPricePerDay;
		var expectedInsuranceTotalPrice = numberOfDays * insurancePricePerDay;
		var expectedTotalPrice = expectedRentalTotalPrice + expectedInsuranceTotalPrice;

		// Act
		var result = this.service.CalculateBill(input);

		// Assert
		result.Should().NotBeNull();
		result.RentalTotalPrice.Should().Be(expectedRentalTotalPrice);
		result.InsuranceTotalPrice.Should().Be(expectedInsuranceTotalPrice);
		result.SummaryTotalPrice.Should().Be(expectedTotalPrice);
	}
}