namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Rentals;

public sealed record InternalRentalIdWithDateTimesDto(
    int Id, 
    DateTime GeneratedAt,
    DateTime ExpiresAt
);