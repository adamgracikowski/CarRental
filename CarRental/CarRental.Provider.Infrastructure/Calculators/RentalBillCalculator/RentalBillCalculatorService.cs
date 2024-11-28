namespace CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;

public sealed class RentalBillCalculatorService : IRentalBillCalculatorService
{
    public RentalBillCanculatorResult CalculateBill(RentalBillCalculatorInput input)
    {
        var difference = input.ReturnedAt - input.RentedAt;
        var numberOfDays = (int) Math.Ceiling(difference.TotalDays);

        var insurancePrice = numberOfDays * input.InsurancePricePerDay;
        var rentalPrice = numberOfDays * input.RentalPricePerDay;
        var totalPrice = insurancePrice + rentalPrice;

        return new RentalBillCanculatorResult(numberOfDays, rentalPrice, insurancePrice, totalPrice);

    }
}