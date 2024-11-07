namespace CarRental.Provider.Infrastructure.Calculators.OfferCalculator;
public interface IOfferCalculatorService
{
    OfferCalculatorResult CalculatePricePerDay(OfferCalculatorInput input);
}