namespace CarRental.Provider.Infrastructure.Calculators.RentalBillCalculator;

public interface IRentalBillCalculatorService
{
    public RentalBillCanculatorResult CalculateBill(RentalBillCalculatorInput input);
}