﻿namespace CarRental.Provider.API.Requests.Rentals.DTOs;

public sealed record RentalDto(
    int Id, 
    DateTime GeneratedAt,
    DateTime ExpiresAt);