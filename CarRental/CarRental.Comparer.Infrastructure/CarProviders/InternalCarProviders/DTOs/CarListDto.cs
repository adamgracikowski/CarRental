namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs;

public sealed record CarListDto(ICollection<MakeDto> Makes);