namespace CarRental.Comparer.API.DTOs.Users;

public sealed record UserForEmployeeDto
{
	public required string Email { get; init; }
	public required string FirstName { get; init; }
	public required string LastName { get; init; }
}