namespace CarRental.Comparer.Web.Requests.DTOs.Users;

public sealed record UserForEmployeeDto(
	string Email,
	string FirstName,
	string LastName
);
