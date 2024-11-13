namespace CarRental.Comparer.Web.Requests.DTOs.Cars;

public sealed record CarDto(
    int Id,
    int ProductionYear,
    string ProviderName
);