namespace CarRental.Provider.API.DTOs.Customers;

public sealed record CustomerDto(
    string EmailAddress,
    string FirstName,
    string LastName
);