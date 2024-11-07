namespace CarRental.Provider.Infrastructure.Calculators.OfferCalculator;

public sealed class OfferCalculatorOptions
{
    public const string SectionName = "OfferCalculator";

    public decimal YoungDriverSurcharge { get; set; }
    
    public int YoungDriverAgeLimit { get; set; }
    
    public decimal InexperiencedDriverSurcharge { get; set; }
    
    public int InexperiencedDriverYearsLimit { get; set; }
    
    public int OfferExpirationInMinutes { get; set; }
}