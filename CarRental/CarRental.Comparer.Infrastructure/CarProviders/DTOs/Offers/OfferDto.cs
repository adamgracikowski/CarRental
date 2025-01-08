using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Cars;
using CarRental.Comparer.Infrastructure.CarProviders.DTOs.Segments;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Offers;

public sealed record OfferDto(
	int Id,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay,
	CarDetailsDto Car,
	SegmentDto? Segment
);
