namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs;

public sealed record MakeDto(
    string Name, 
    ICollection<ModelDto> Models
);