﻿using CarRental.Comparer.Web.Requests.DTOs.Cars;
using CarRental.Comparer.Web.Requests.DTOs.Segments;

namespace CarRental.Comparer.Web.Requests.DTOs.Offers;

public sealed record OfferDto(
	int Id,
	decimal RentalPricePerDay,
	decimal InsurancePricePerDay,
	DateTime GeneratedAt,
	DateTime ExpiresAt,
	CarDetailsDto Car,
	SegmentDto Segment
);