namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Cars;

public sealed record CarListDto(ICollection<MakeDto> Makes);