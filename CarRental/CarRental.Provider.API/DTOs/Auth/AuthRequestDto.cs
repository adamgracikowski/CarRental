namespace CarRental.Provider.API.DTOs.Auth;

public sealed record AuthRequestDto(string ClientId, string ClientSecret);