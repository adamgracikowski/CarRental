using CarRental.Common.Core.Enums;
using System.Text.Json.Serialization;

namespace CarRental.Provider.API.Requests.Rentals.DTOs;

public sealed record RentalStatusDto(
    int Id,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] RentalStatus Status
);