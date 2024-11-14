namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;

public sealed record MakeDto(
    string Name, 
    ICollection<ModelDto> Models
);