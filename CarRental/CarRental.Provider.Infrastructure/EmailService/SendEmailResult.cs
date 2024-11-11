namespace CarRental.Provider.Infrastructure.EmailService;

public sealed record SendEmailResult(
    bool Success,
    string ErrorMessages
);
