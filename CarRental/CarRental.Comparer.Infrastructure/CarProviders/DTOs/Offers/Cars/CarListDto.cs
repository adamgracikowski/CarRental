namespace CarRental.Comparer.Infrastructure.CarProviders.DTOs.Offers.Cars;

public sealed record CarListDto(
    ICollection<MakeDto> Makes
);