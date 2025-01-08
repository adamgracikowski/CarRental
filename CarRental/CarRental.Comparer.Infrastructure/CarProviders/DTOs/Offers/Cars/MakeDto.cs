namespace CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers.Cars;

public sealed record MakeDto(
    string Name,
    ICollection<ModelDto> Models
);