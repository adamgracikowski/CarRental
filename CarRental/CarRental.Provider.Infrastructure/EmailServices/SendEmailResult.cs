namespace CarRental.Provider.Infrastructure.EmailServices;

public sealed record SendEmailResult(
	bool Success,
	string ErrorMessages
);