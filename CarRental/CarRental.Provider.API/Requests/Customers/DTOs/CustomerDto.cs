namespace CarRental.Provider.API.Requests.Customers.DTOs;

public sealed record CustomerDto(
    string EmailAddress,
    string FirstName,
    string LastName
);