using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

public sealed record InternalOfferDto(
	int Id,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay,
	DateTime GeneratedAt,
	DateTime ExpiresAt,
	CarDetailsDto Car,
	SegmentDto Segment
);