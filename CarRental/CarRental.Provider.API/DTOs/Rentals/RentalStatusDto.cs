using CarRental.Common.Core.Enums;
using System.Text.Json.Serialization;

namespace CarRental.Provider.API.DTOs.Rentals;

public sealed record RentalStatusDto(
    int Id,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] RentalStatus Status
);